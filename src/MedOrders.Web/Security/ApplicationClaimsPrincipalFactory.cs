using System.Security.Claims;
using MedOrders.Web.Domain;
using MedOrders.Web.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MedOrders.Web.Security;

/// <summary>Формирует данные пользователя для cookie: роль и ФИО.</summary>
public sealed class ApplicationClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IOptions<IdentityOptions> options,
    AppDbContext db)
    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, options)
{
    /// <summary>Добавляет к стандартным claim-ам роль и ФИО пользователя.</summary>
    /// <param name="user">Учётная запись.</param>
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        var roleName = await db.Roles.Where(role => role.Id == user.RoleId)
            .Select(role => role.Name)
            .SingleOrDefaultAsync();

        if (roleName is not null)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
        }

        identity.AddClaim(new Claim(CustomClaimTypes.FullName, user.FullName));
        return identity;
    }
}
