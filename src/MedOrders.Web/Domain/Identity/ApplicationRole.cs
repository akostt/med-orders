using Microsoft.AspNetCore.Identity;

namespace MedOrders.Web.Domain;

/// <summary>Роль пользователя (таблица Role).</summary>
public sealed class ApplicationRole : IdentityRole<int>
{
    /// <summary>Создаёт пустую роль. Нужен Entity Framework.</summary>
    public ApplicationRole()
    {
    }

    /// <summary>Создаёт роль с названием и описанием прав.</summary>
    /// <param name="name">Название роли, например «клиент».</param>
    /// <param name="accessRights">Описание прав доступа.</param>
    public ApplicationRole(string name, string accessRights) : base(name)
    {
        AccessRights = accessRights;
    }

    /// <summary>Описание набора прав доступа, назначаемого роли.</summary>
    public string AccessRights { get; set; } = string.Empty;
}
