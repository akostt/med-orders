using MedOrders.Web.Components;
using MedOrders.Web.Domain;
using MedOrders.Web.Endpoints;
using MedOrders.Web.Persistence;
using MedOrders.Web.Security;
using MedOrders.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

const int PasswordMinLength = 8;
const int MaxFailedLoginAttempts = 5;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MedOrders")
    ?? throw new InvalidOperationException("Не задана строка подключения ConnectionStrings:MedOrders.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddSingleton(new PricingSettings(
    builder.Configuration.GetValue<decimal>("Tax:VatRatePercent"),
    builder.Configuration.GetValue<decimal>("Delivery:CourierCost"),
    builder.Configuration.GetValue<decimal>("Delivery:TransportCompanyCost"),
    builder.Configuration.GetValue<decimal>("Delivery:PickupCost")));
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = PasswordMinLength;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = MaxFailedLoginAttempts;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<RussianIdentityErrorDescriber>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsPrincipalFactory>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/";
});
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Применяет миграции: таблицы, демо-каталог и учётные записи (HasData попадает в миграцию).
    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapAccountEndpoints();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
