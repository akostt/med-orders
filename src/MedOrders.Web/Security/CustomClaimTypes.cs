namespace MedOrders.Web.Security;

/// <summary>Дополнительные claim-ы, которые кладутся в cookie при входе.</summary>
public static class CustomClaimTypes
{
    /// <summary>ФИО пользователя для показа в шапке сайта.</summary>
    public const string FullName = "FullName";
}
