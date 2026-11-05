using System.Globalization;
using MedOrders.Web.Domain;
using MedOrders.Web.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedOrders.Web.Services;

/// <summary>
/// Оформление заказа клиентом по диаграмме деятельности «Оформление заказа» (ЛР4).
/// Шаги: проверка полей → расчёт стоимости → проверка наличия → резерв на складе → регистрация заказа
/// со статусом «принят». Резерв и регистрация выполняются в одной транзакции.
/// </summary>
public sealed class OrderService(AppDbContext db, PricingSettings pricing, TimeProvider clock)
{
    private const int OrderNumberDigits = 4;

    /// <summary>Скидку назначает менеджер, поэтому новый заказ создаётся без скидки.</summary>
    private const decimal NewOrderDiscountPercent = 0m;

    /// <summary>Создаёт заказ и резервирует товар на складе.</summary>
    /// <param name="command">Данные формы оформления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Номер созданного заказа, например ORD-2026-0001.</returns>
    /// <exception cref="FormValidationException">Не заполнены обязательные поля.</exception>
    /// <exception cref="BusinessRuleException">Клиент не найден, товар недоступен или не хватает на складе.</exception>
    public async Task<string> CreateOrderAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        var fieldErrors = OrderFormValidator.Validate(command);
        if (fieldErrors.Count > 0)
        {
            throw new FormValidationException(fieldErrors);
        }

        var clientId = await GetClientIdAsync(command.UserId, cancellationToken);
        var product = await GetActiveProductAsync(command.ProductId, cancellationToken);

        var deliveryCost = pricing.GetDeliveryCost(command.DeliveryMethod);
        var totals = OrderTotalsCalculator.Calculate(
            product.Price, command.Quantity, NewOrderDiscountPercent, pricing.VatRatePercent, deliveryCost);

        var now = clock.GetUtcNow().UtcDateTime;

        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var warehouseId = await ReserveStockAsync(product, command.Quantity, now, cancellationToken);
        var order = await BuildOrderAsync(clientId, command, product, warehouseId, totals, deliveryCost, now, cancellationToken);

        db.Orders.Add(order);
        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return order.Number;
    }

    private async Task<int> GetClientIdAsync(int userId, CancellationToken cancellationToken)
    {
        var clientId = await db.Clients.AsNoTracking()
            .Where(client => client.UserId == userId)
            .Select(client => client.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return clientId == 0
            ? throw new BusinessRuleException("Заказы оформляет клиент. Войдите под учётной записью клиента.")
            : clientId;
    }

    private async Task<Product> GetActiveProductAsync(int productId, CancellationToken cancellationToken) =>
        await db.Products.AsNoTracking()
            .Include(product => product.Stocks)
            .FirstOrDefaultAsync(product => product.Id == productId && product.IsActive, cancellationToken)
        ?? throw new BusinessRuleException("Товар недоступен для заказа.");

    /// <summary>
    /// Резервирует товар на первом складе с достаточным остатком (по возрастанию ID склада).
    /// Условие повторно проверяется в UPDATE, поэтому два заказа не зарезервируют одну и ту же единицу.
    /// </summary>
    private async Task<int> ReserveStockAsync(Product product, int quantity, DateTime now, CancellationToken cancellationToken)
    {
        var suitableStock = await db.Stocks.AsNoTracking()
            .Where(stock => stock.ProductId == product.Id && stock.Quantity - stock.ReservedQuantity >= quantity)
            .OrderBy(stock => stock.WarehouseId)
            .FirstOrDefaultAsync(cancellationToken);

        if (suitableStock is null)
        {
            var availableQuantity = product.Stocks.Sum(stock => stock.Available);
            throw new BusinessRuleException(
                $"Недостаточно товара: доступно {availableQuantity} шт., в заказе {quantity} шт. Измените количество.");
        }

        var updatedRows = await db.Stocks
            .Where(stock => stock.Id == suitableStock.Id && stock.Quantity - stock.ReservedQuantity >= quantity)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(stock => stock.ReservedQuantity, stock => stock.ReservedQuantity + quantity)
                .SetProperty(stock => stock.UpdatedAt, now), cancellationToken);

        if (updatedRows == 0)
        {
            throw new BusinessRuleException("Остаток изменился, обновите страницу и повторите заказ.");
        }

        return suitableStock.WarehouseId;
    }

    private async Task<Order> BuildOrderAsync(
        int clientId,
        CreateOrderCommand command,
        Product product,
        int warehouseId,
        OrderTotals totals,
        decimal deliveryCost,
        DateTime now,
        CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Number = await GenerateOrderNumberAsync(now, cancellationToken),
            ClientId = clientId,
            Status = OrderStatus.Accepted,
            DeliveryMethod = command.DeliveryMethod,
            DeliveryAddress = command.DeliveryAddress.Trim(),
            Notes = string.IsNullOrWhiteSpace(command.Notes) ? null : command.Notes.Trim(),
            DeliveryCost = deliveryCost,
            Discount = totals.Discount,
            DiscountPercent = NewOrderDiscountPercent,
            TaxAmount = totals.Vat,
            TotalAmount = totals.Total,
            PaymentStatus = PaymentStatus.NotPaid,
            CreatedAt = now,
        };

        order.Items.Add(new OrderItem
        {
            ProductId = product.Id,
            WarehouseId = warehouseId,
            Quantity = command.Quantity,
            UnitPrice = product.Price,
            LineTotal = totals.Subtotal,
        });

        return order;
    }

    /// <summary>Номер заказа: префикс ORD, год и порядковый номер внутри года.</summary>
    private async Task<string> GenerateOrderNumberAsync(DateTime now, CancellationToken cancellationToken)
    {
        var prefix = $"ORD-{now.Year}-";
        var lastNumber = await db.Orders.AsNoTracking()
            .Where(order => order.Number.StartsWith(prefix))
            .OrderByDescending(order => order.Number)
            .Select(order => order.Number)
            .FirstOrDefaultAsync(cancellationToken);

        var nextSequence = lastNumber is null
            ? 1
            : int.Parse(lastNumber[prefix.Length..], CultureInfo.InvariantCulture) + 1;

        return prefix + nextSequence.ToString($"D{OrderNumberDigits}", CultureInfo.InvariantCulture);
    }
}
