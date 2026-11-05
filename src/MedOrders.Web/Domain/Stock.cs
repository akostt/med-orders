namespace MedOrders.Web.Domain;

/// <summary>Остаток товара на складе (таблица Stock, связь товар–склад).</summary>
public sealed class Stock
{
    /// <summary>Идентификатор записи (ID_Stock).</summary>
    public int Id { get; set; }

    /// <summary>Товар.</summary>
    public int ProductId { get; set; }

    /// <summary>Склад.</summary>
    public int WarehouseId { get; set; }

    /// <summary>Физическое количество на складе, штук.</summary>
    public int Quantity { get; set; }

    /// <summary>Количество, зарезервированное под заказы, штук.</summary>
    public int ReservedQuantity { get; set; }

    /// <summary>Дата и время последнего изменения остатка (UTC).</summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>Доступно для новых заказов: физический остаток минус резерв.</summary>
    public int Available => Quantity - ReservedQuantity;
}
