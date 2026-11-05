using System.Security.Claims;

namespace MedOrders.Web.Security;

/// <summary>Удобное чтение данных вошедшего пользователя.</summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>Возвращает идентификатор учётной записи.</summary>
    /// <param name="principal">Текущий пользователь.</param>
    /// <returns>Идентификатор пользователя (ID_User).</returns>
    /// <exception cref="InvalidOperationException">Пользователь не авторизован.</exception>
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        var value = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new InvalidOperationException("Пользователь не авторизован.");
        return int.Parse(value);
    }

    /// <summary>Возвращает ФИО пользователя или пустую строку.</summary>
    /// <param name="principal">Текущий пользователь.</param>
    public static string GetFullName(this ClaimsPrincipal principal) =>
        principal.FindFirst(CustomClaimTypes.FullName)?.Value ?? string.Empty;

    /// <summary>Возвращает название роли пользователя или пустую строку.</summary>
    /// <param name="principal">Текущий пользователь.</param>
    public static string GetRoleName(this ClaimsPrincipal principal) =>
        principal.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
}
