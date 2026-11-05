using MedOrders.Web.Domain;
using MedOrders.Web.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedOrders.Web.Services;

/// <summary>
/// Добавление товара в каталог по диаграмме деятельности «Добавление товара» (ЛР4).
/// Шаги: проверка полей и уникальности артикула → сохранение товара и документа → запись в журнал действий.
/// </summary>
public sealed class ProductService(AppDbContext db, TimeProvider clock)
{
    private const int ArticleMaxLength = 50;
    private const int NameMaxLength = 250;
    private const int MaxWarrantyMonths = 120;
    private const int CertificateNumberMaxLength = 50;

    /// <summary>Создаёт товар вместе с документом и начальным остатком и пишет запись в журнал.</summary>
    /// <param name="command">Данные формы.</param>
    /// <param name="actorUserId">Пользователь, который создаёт товар.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Артикул созданного товара.</returns>
    /// <exception cref="FormValidationException">Есть ошибки заполнения полей.</exception>
    public async Task<string> CreateProductAsync(
        CreateProductCommand command,
        int actorUserId,
        CancellationToken cancellationToken = default)
    {
        var fieldErrors = await ValidateAsync(command, cancellationToken);
        if (fieldErrors.Count > 0)
        {
            throw new FormValidationException(fieldErrors);
        }

        var now = clock.GetUtcNow().UtcDateTime;
        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var product = BuildProduct(command);
        if (command.InitialQuantity > 0)
        {
            product.Stocks.Add(await BuildInitialStockAsync(command.InitialQuantity, now, cancellationToken));
        }

        db.Products.Add(product);
        await db.SaveChangesAsync(cancellationToken);

        db.ActionLogs.Add(ActionLog.Create(actorUserId, ActionLog.Actions.Create, ActionLog.Tables.Product, product.Id, now));
        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return product.Article;
    }

    private static Product BuildProduct(CreateProductCommand command)
    {
        var product = new Product
        {
            Article = command.Article.Trim(),
            Name = command.Name.Trim(),
            CategoryId = command.CategoryId,
            ManufacturerId = command.ManufacturerId,
            Price = command.Price,
            WarrantyMonths = command.WarrantyMonths,
            ShelfLifeMonths = command.ShelfLifeMonths,
            StorageConditions = string.IsNullOrWhiteSpace(command.StorageConditions) ? null : command.StorageConditions.Trim(),
            IsActive = true,
        };

        if (command.CertificateType is { } certificateType)
        {
            var number = command.CertificateNumber!.Trim();
            product.Certificates.Add(new Certificate
            {
                DocType = certificateType,
                Number = number,
                IssuedAt = command.CertificateIssuedAt!.Value,
            });

            if (certificateType == CertificateType.RegistrationCertificate)
            {
                product.RegistrationNumber = number;
            }
        }

        return product;
    }

    private async Task<Stock> BuildInitialStockAsync(int quantity, DateTime now, CancellationToken cancellationToken)
    {
        var mainWarehouseId = await db.Warehouses.OrderBy(warehouse => warehouse.Id)
            .Select(warehouse => warehouse.Id)
            .FirstAsync(cancellationToken);

        return new Stock
        {
            WarehouseId = mainWarehouseId,
            Quantity = quantity,
            ReservedQuantity = 0,
            UpdatedAt = now,
        };
    }

    private async Task<Dictionary<string, string>> ValidateAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var errors = new Dictionary<string, string>();

        FieldValidation.RequireText(errors, nameof(command.Article), command.Article, ArticleMaxLength, "Артикул");
        FieldValidation.RequireText(errors, nameof(command.Name), command.Name, NameMaxLength, "Наименование");

        if (!errors.ContainsKey(nameof(command.Article))
            && await db.Products.AnyAsync(product => product.Article == command.Article.Trim(), cancellationToken))
        {
            errors[nameof(command.Article)] = "Товар с таким артикулом уже есть в каталоге.";
        }

        if (command.CategoryId <= 0
            || !await db.Categories.AnyAsync(category => category.Id == command.CategoryId, cancellationToken))
        {
            errors[nameof(command.CategoryId)] = "Выберите категорию из списка.";
        }

        if (command.ManufacturerId <= 0
            || !await db.Manufacturers.AnyAsync(manufacturer => manufacturer.Id == command.ManufacturerId, cancellationToken))
        {
            errors[nameof(command.ManufacturerId)] = "Выберите производителя из списка.";
        }

        if (command.Price <= 0)
        {
            errors[nameof(command.Price)] = "Цена должна быть больше нуля.";
        }

        if (command.WarrantyMonths < 0 || command.WarrantyMonths > MaxWarrantyMonths)
        {
            errors[nameof(command.WarrantyMonths)] = $"Гарантия от 0 до {MaxWarrantyMonths} месяцев.";
        }

        if (command.ShelfLifeMonths <= 0)
        {
            errors[nameof(command.ShelfLifeMonths)] = "Укажите срок годности в месяцах.";
        }

        if (command.InitialQuantity < 0)
        {
            errors[nameof(command.InitialQuantity)] = "Количество не может быть отрицательным.";
        }

        if (command.CertificateType is not null)
        {
            ValidateCertificate(errors, command);
        }

        return errors;
    }

    private static void ValidateCertificate(Dictionary<string, string> errors, CreateProductCommand command)
    {
        FieldValidation.RequireText(
            errors, nameof(command.CertificateNumber), command.CertificateNumber,
            CertificateNumberMaxLength, "Номер документа");

        if (command.CertificateIssuedAt is null)
        {
            errors[nameof(command.CertificateIssuedAt)] = "Укажите дату выдачи документа.";
        }
        else if (command.CertificateIssuedAt > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            errors[nameof(command.CertificateIssuedAt)] = "Дата выдачи не может быть в будущем.";
        }
    }
}
