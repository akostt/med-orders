namespace MedOrders.Web.Domain;

/// <summary>Товар (медицинская техника) каталога (таблица Product).</summary>
public sealed class Product
{
    /// <summary>Идентификатор товара (ID_Product).</summary>
    public int Id { get; set; }

    /// <summary>Артикул (SKU), уникален в каталоге.</summary>
    public string Article { get; set; } = string.Empty;

    /// <summary>Наименование товара.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Идентификатор категории.</summary>
    public int CategoryId { get; set; }

    /// <summary>Идентификатор производителя.</summary>
    public int ManufacturerId { get; set; }

    /// <summary>Цена за единицу, рублей.</summary>
    public decimal Price { get; set; }

    /// <summary>Номер регистрационного удостоверения, если есть.</summary>
    public string? RegistrationNumber { get; set; }

    /// <summary>Условия хранения и транспортировки.</summary>
    public string? StorageConditions { get; set; }

    /// <summary>Гарантийный срок в месяцах.</summary>
    public int WarrantyMonths { get; set; }

    /// <summary>Срок годности в месяцах.</summary>
    public int ShelfLifeMonths { get; set; }

    /// <summary>Технические характеристики: по одной строке на параметр, в виде «Название: значение».</summary>
    public string? Specifications { get; set; }

    /// <summary>Признак доступности товара для заказа. Товар снимают с продажи, а не удаляют.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Категория товара (навигационное свойство).</summary>
    public Category? Category { get; set; }

    /// <summary>Производитель товара (навигационное свойство).</summary>
    public Manufacturer? Manufacturer { get; set; }

    /// <summary>Регистрационные документы товара.</summary>
    public List<Certificate> Certificates { get; set; } = [];

    /// <summary>Остатки товара по складам.</summary>
    public List<Stock> Stocks { get; set; } = [];
}
