namespace MedOrders.Web.Domain;

/// <summary>Склад, на котором хранится товар (таблица Warehouse).</summary>
public sealed class Warehouse
{
    /// <summary>Идентификатор склада (ID_Warehouse).</summary>
    public int Id { get; set; }

    /// <summary>Название склада.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Адрес склада.</summary>
    public string Address { get; set; } = string.Empty;
}
