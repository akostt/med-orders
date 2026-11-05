namespace MedOrders.Web.Domain;

/// <summary>Статус заказа. Жизненный цикл описан в правилах заказа (раздел 6 ТЗ).</summary>
public enum OrderStatus
{
    /// <summary>Заказ принят и ждёт обработки.</summary>
    Accepted,

    /// <summary>Менеджер взял заказ в обработку.</summary>
    InProgress,

    /// <summary>Товар скомплектован.</summary>
    Packed,

    /// <summary>Заказ отправлен клиенту.</summary>
    Shipped,

    /// <summary>Клиент подтвердил получение.</summary>
    Delivered,

    /// <summary>Заказ отменён, резерв снят.</summary>
    Cancelled,
}

/// <summary>Статус оплаты заказа.</summary>
public enum PaymentStatus
{
    /// <summary>Оплата не поступила.</summary>
    NotPaid,

    /// <summary>Оплата подтверждена менеджером.</summary>
    Paid,

    /// <summary>Оплата возвращена после отмены.</summary>
    Refunded,
}

/// <summary>Способ доставки заказа.</summary>
public enum DeliveryMethod
{
    /// <summary>Доставка курьером.</summary>
    Courier,

    /// <summary>Доставка транспортной компанией.</summary>
    TransportCompany,

    /// <summary>Самовывоз со склада.</summary>
    Pickup,
}

/// <summary>Вид документа на товар: регистрационное удостоверение, сертификат или декларация.</summary>
public enum CertificateType
{
    /// <summary>Регистрационное удостоверение (РУ) Росздравнадзора.</summary>
    RegistrationCertificate,

    /// <summary>Сертификат соответствия (СС).</summary>
    ConformityCertificate,

    /// <summary>Декларация о соответствии.</summary>
    Declaration,
}

/// <summary>Тип клиента.</summary>
public enum ClientType
{
    /// <summary>Медицинское учреждение.</summary>
    MedicalInstitution,

    /// <summary>Индивидуальный предприниматель.</summary>
    SoleProprietor,

    /// <summary>Физическое лицо.</summary>
    Individual,
}
