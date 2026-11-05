namespace MedOrders.Web.Domain;

/// <summary>Заказ клиента (таблица Order).</summary>
public sealed class Order
{
    /// <summary>Идентификатор заказа (ID_Order).</summary>
    public int Id { get; set; }

    /// <summary>Номер заказа, например ORD-2026-0001. Уникален.</summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>Клиент, оформивший заказ.</summary>
    public int ClientId { get; set; }

    /// <summary>Текущий статус заказа.</summary>
    public OrderStatus Status { get; set; } = OrderStatus.Accepted;

    /// <summary>Способ доставки.</summary>
    public DeliveryMethod DeliveryMethod { get; set; }

    /// <summary>Адрес доставки.</summary>
    public string DeliveryAddress { get; set; } = string.Empty;

    /// <summary>Стоимость доставки, рублей.</summary>
    public decimal DeliveryCost { get; set; }

    /// <summary>Сумма скидки, рублей.</summary>
    public decimal Discount { get; set; }

    /// <summary>Скидка в процентах, которую назначил менеджер.</summary>
    public decimal DiscountPercent { get; set; }

    /// <summary>Сумма НДС, рублей.</summary>
    public decimal TaxAmount { get; set; }

    /// <summary>Итого к оплате, рублей.</summary>
    public decimal TotalAmount { get; set; }

    /// <summary>Статус оплаты.</summary>
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.NotPaid;

    /// <summary>Дата и время создания заказа (UTC).</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>Комментарий клиента, необязательный.</summary>
    public string? Notes { get; set; }

    /// <summary>Позиции заказа.</summary>
    public List<OrderItem> Items { get; set; } = [];
}
