namespace MedOrders.Web.Services;

/// <summary>Пункт выпадающего списка: категория или производитель.</summary>
/// <param name="Id">Идентификатор.</param>
/// <param name="Name">Название для показа.</param>
public sealed record SelectItem(int Id, string Name);

/// <summary>Порядок сортировки каталога.</summary>
public enum CatalogSortOrder
{
    /// <summary>По названию от А до Я.</summary>
    ByName,

    /// <summary>По цене от меньшей к большей.</summary>
    PriceAscending,

    /// <summary>По цене от большей к меньшей.</summary>
    PriceDescending,
}

/// <summary>Параметры размера страницы каталога.</summary>
public static class CatalogPaging
{
    /// <summary>Количество карточек на одной странице каталога.</summary>
    public const int DefaultPageSize = 9;
}

/// <summary>Условия поиска товаров. Все поля необязательны.</summary>
public sealed record CatalogFilter
{
    /// <summary>Текст для поиска по названию или артикулу.</summary>
    public string? SearchText { get; init; }

    /// <summary>Идентификатор категории.</summary>
    public int? CategoryId { get; init; }

    /// <summary>Идентификатор производителя.</summary>
    public int? ManufacturerId { get; init; }

    /// <summary>Нижняя граница цены, рублей.</summary>
    public decimal? MinPrice { get; init; }

    /// <summary>Верхняя граница цены, рублей.</summary>
    public decimal? MaxPrice { get; init; }

    /// <summary>Показывать только товары, которые есть на складе.</summary>
    public bool OnlyInStock { get; init; }

    /// <summary>Показывать только товары с регистрационным удостоверением.</summary>
    public bool OnlyWithRegistration { get; init; }

    /// <summary>Порядок сортировки.</summary>
    public CatalogSortOrder SortOrder { get; init; } = CatalogSortOrder.ByName;

    /// <summary>Номер страницы, начиная с единицы.</summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>Размер страницы.</summary>
    public int PageSize { get; init; } = CatalogPaging.DefaultPageSize;
}

/// <summary>Карточка товара в списке каталога.</summary>
/// <param name="Id">Идентификатор товара.</param>
/// <param name="Article">Артикул.</param>
/// <param name="Name">Наименование.</param>
/// <param name="Price">Цена, рублей.</param>
/// <param name="AvailableQuantity">Доступное количество на всех складах.</param>
/// <param name="HasRegistrationCertificate">Есть регистрационное удостоверение (РУ).</param>
/// <param name="HasConformityCertificate">Есть сертификат соответствия (СС).</param>
public sealed record ProductCard(
    int Id,
    string Article,
    string Name,
    decimal Price,
    int AvailableQuantity,
    bool HasRegistrationCertificate,
    bool HasConformityCertificate);

/// <summary>Одна страница результатов поиска каталога.</summary>
/// <param name="Items">Карточки товаров на странице.</param>
/// <param name="TotalCount">Всего найдено товаров по условиям поиска.</param>
/// <param name="PageNumber">Номер текущей страницы.</param>
/// <param name="PageSize">Размер страницы.</param>
public sealed record CatalogResult(
    IReadOnlyList<ProductCard> Items,
    int TotalCount,
    int PageNumber,
    int PageSize)
{
    /// <summary>Количество страниц, не меньше одной.</summary>
    public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
}

/// <summary>Полная карточка товара для страницы оформления.</summary>
/// <param name="Id">Идентификатор товара.</param>
/// <param name="Article">Артикул.</param>
/// <param name="Name">Наименование.</param>
/// <param name="CategoryName">Название категории.</param>
/// <param name="ManufacturerName">Название производителя.</param>
/// <param name="Price">Цена, рублей.</param>
/// <param name="WarrantyMonths">Гарантия, месяцев.</param>
/// <param name="ShelfLifeMonths">Срок годности, месяцев.</param>
/// <param name="StorageConditions">Условия хранения.</param>
/// <param name="RegistrationNumber">Номер регистрационного удостоверения.</param>
/// <param name="AvailableQuantity">Доступное количество на всех складах.</param>
/// <param name="Specifications">Технические характеристики в исходном текстовом виде.</param>
public sealed record ProductDetails(
    int Id,
    string Article,
    string Name,
    string CategoryName,
    string ManufacturerName,
    decimal Price,
    int WarrantyMonths,
    int ShelfLifeMonths,
    string? StorageConditions,
    string? RegistrationNumber,
    int AvailableQuantity,
    string? Specifications);
