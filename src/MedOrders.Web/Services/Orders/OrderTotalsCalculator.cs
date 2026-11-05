namespace MedOrders.Web.Services;

/// <summary>
/// Расчёт итога заказа. Формула: сумма позиций − скидка + НДС + доставка.
/// НДС считается с суммы после скидки, доставка НДС не облагается (как в макете ЛР4).
/// </summary>
public static class OrderTotalsCalculator
{
    private const decimal PercentBase = 100m;
    private const int MoneyDecimalPlaces = 2;

    /// <summary>Считает итог для одной позиции заказа.</summary>
    /// <param name="unitPrice">Цена за единицу, рублей.</param>
    /// <param name="quantity">Количество, штук.</param>
    /// <param name="discountPercent">Скидка в процентах от 0 до 100.</param>
    /// <param name="vatPercent">Ставка НДС в процентах.</param>
    /// <param name="deliveryCost">Стоимость доставки, рублей.</param>
    /// <returns>Итоги с округлением до копеек.</returns>
    public static OrderTotals Calculate(
        decimal unitPrice,
        int quantity,
        decimal discountPercent,
        decimal vatPercent,
        decimal deliveryCost)
    {
        var subtotal = RoundMoney(unitPrice * quantity);
        var discount = RoundMoney(subtotal * discountPercent / PercentBase);
        var taxableAmount = subtotal - discount;
        var vat = RoundMoney(taxableAmount * vatPercent / PercentBase);
        var total = taxableAmount + vat + deliveryCost;

        return new OrderTotals(subtotal, discount, vat, deliveryCost, total);
    }

    private static decimal RoundMoney(decimal amount) =>
        Math.Round(amount, MoneyDecimalPlaces, MidpointRounding.AwayFromZero);
}
