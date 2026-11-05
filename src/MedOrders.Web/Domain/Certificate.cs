namespace MedOrders.Web.Domain;

/// <summary>Регистрационный документ товара: РУ, сертификат соответствия или декларация (таблица Certificate).</summary>
public sealed class Certificate
{
    /// <summary>Идентификатор записи (ID_Certificate).</summary>
    public int Id { get; set; }

    /// <summary>Товар, к которому относится документ.</summary>
    public int ProductId { get; set; }

    /// <summary>Вид документа.</summary>
    public CertificateType DocType { get; set; }

    /// <summary>Номер документа.</summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>Дата выдачи.</summary>
    public DateOnly IssuedAt { get; set; }

    /// <summary>Дата окончания срока действия, если указана.</summary>
    public DateOnly? ExpiresAt { get; set; }
}
