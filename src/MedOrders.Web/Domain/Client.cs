namespace MedOrders.Web.Domain;

/// <summary>Профиль клиента, связанный с учётной записью один-к-одному (таблица Client).</summary>
public sealed class Client
{
    /// <summary>Идентификатор клиента (ID_Client).</summary>
    public int Id { get; set; }

    /// <summary>Учётная запись клиента (ID_User).</summary>
    public int UserId { get; set; }

    /// <summary>Тип клиента.</summary>
    public ClientType ClientType { get; set; }

    /// <summary>Адрес клиента.</summary>
    public string Address { get; set; } = string.Empty;
}
