using MedOrders.Web.Domain;

namespace MedOrders.Web.Services;

/// <summary>Данные формы «Новый товар» (диаграмма «Добавление товара», ЛР4).</summary>
/// <param name="Article">Артикул (SKU), уникален в каталоге.</param>
/// <param name="Name">Наименование товара.</param>
/// <param name="CategoryId">Категория.</param>
/// <param name="ManufacturerId">Производитель.</param>
/// <param name="Price">Цена, рублей.</param>
/// <param name="WarrantyMonths">Гарантия, месяцев.</param>
/// <param name="ShelfLifeMonths">Срок годности, месяцев.</param>
/// <param name="StorageConditions">Условия хранения, необязательно.</param>
/// <param name="InitialQuantity">Начальный остаток на основном складе, штук.</param>
/// <param name="CertificateType">Вид прикрепляемого документа или null, если документ не прикрепляется.</param>
/// <param name="CertificateNumber">Номер документа.</param>
/// <param name="CertificateIssuedAt">Дата выдачи документа.</param>
public sealed record CreateProductCommand(
    string Article,
    string Name,
    int CategoryId,
    int ManufacturerId,
    decimal Price,
    int WarrantyMonths,
    int ShelfLifeMonths,
    string? StorageConditions,
    int InitialQuantity,
    CertificateType? CertificateType,
    string? CertificateNumber,
    DateOnly? CertificateIssuedAt);
