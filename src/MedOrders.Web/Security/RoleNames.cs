namespace MedOrders.Web.Security;

/// <summary>Названия ролей. Совпадают со значениями в таблице Role.</summary>
public static class RoleNames
{
    /// <summary>Клиент: каталог, оформление и отслеживание собственных заказов.</summary>
    public const string Client = "клиент";

    /// <summary>Менеджер: товары, обработка заказов, документы и отчёты.</summary>
    public const string Manager = "менеджер";

    /// <summary>Администратор: пользователи, роли и журнал действий.</summary>
    public const string Administrator = "администратор";
}
