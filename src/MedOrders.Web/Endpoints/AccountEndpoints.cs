using MedOrders.Web.Domain;
using MedOrders.Web.Security;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;

namespace MedOrders.Web.Endpoints;

/// <summary>
/// Обычные HTTP-адреса входа и выхода. Вход реализует диаграмму деятельности «Авторизация» (ЛР4).
/// </summary>
public static class AccountEndpoints
{
    /// <summary>Регистрирует адреса /account/login и /account/logout.</summary>
    /// <param name="app">Маршрутизатор приложения.</param>
    public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/account/authenticate", LoginAsync);
        app.MapPost("/account/logout", LogoutAsync).RequireAuthorization();
    }

    private static async Task<IResult> LoginAsync(
        HttpContext httpContext,
        IAntiforgery antiforgery,
        SignInManager<ApplicationUser> signInManager,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(nameof(AccountEndpoints));

        if (!await antiforgery.IsRequestValidAsync(httpContext))
        {
            return Results.BadRequest();
        }

        var form = await httpContext.Request.ReadFormAsync();
        var login = form["Login"].ToString().Trim();
        var password = form["Password"].ToString();

        var signInResult = await signInManager.PasswordSignInAsync(login, password, isPersistent: false, lockoutOnFailure: true);
        if (signInResult.Succeeded)
        {
            logger.LogInformation("Выполнен вход: {Login}", login);
            return Results.Redirect("/");
        }

        var failureReason = signInResult.IsLockedOut ? "locked" : signInResult.IsNotAllowed ? "blocked" : "invalid";
        logger.LogWarning("Неудачная попытка входа: {Login}, причина: {Reason}", login, failureReason);
        return Results.Redirect($"/account/login?error={failureReason}");
    }

    private static async Task<IResult> LogoutAsync(
        HttpContext httpContext,
        IAntiforgery antiforgery,
        SignInManager<ApplicationUser> signInManager)
    {
        if (!await antiforgery.IsRequestValidAsync(httpContext))
        {
            return Results.BadRequest();
        }

        await signInManager.SignOutAsync();
        return Results.Redirect("/account/login");
    }
}
