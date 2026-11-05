using Microsoft.AspNetCore.Identity;

namespace MedOrders.Web.Domain;

/// <summary>Учётная запись пользователя (таблица User). У пользователя одна роль.</summary>
public sealed class ApplicationUser : IdentityUser<int>
{
    /// <summary>Фамилия, имя и отчество.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>Дата и время регистрации (UTC).</summary>
    public DateTime RegisteredAt { get; set; }

    /// <summary>Признак активной учётной записи. Неактивные не могут войти.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Идентификатор роли (ID_Role).</summary>
    public int RoleId { get; set; }

    /// <summary>Роль пользователя (навигационное свойство).</summary>
    public ApplicationRole? Role { get; set; }
}
