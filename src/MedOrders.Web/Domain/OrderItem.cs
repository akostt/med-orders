namespace MedOrders.Web.Domain;

/// <summary>Позиция заказа: товар, количество и цена на момент заказа (таблица OrderItem).</summary>
public sealed class OrderItem
{
    /// <summary>Идентификатор позиции (ID_OrderItem).</summary>
    public int Id { get; set; }

    /// <summary>Заказ, которому принадлежит позиция.</summary>
    public int OrderId { get; set; }

    /// <summary>Товар.</summary>
    public int ProductId { get; set; }

    /// <summary>Склад, с которого зарезервирован товар. Нужен, чтобы снять резерв с того же склада.</summary>
    public int WarehouseId { get; set; }

    /// <summary>Количество, штук.</summary>
    public int Quantity { get; set; }

    /// <summary>Цена за единицу на момент заказа, рублей.</summary>
    public decimal UnitPrice { get; set; }

    /// <summary>Сумма по позиции без скидки, рублей.</summary>
    public decimal LineTotal { get; set; }

    /// <summary>Товар (навигационное свойство).</summary>
    public Product? Product { get; set; }
}
