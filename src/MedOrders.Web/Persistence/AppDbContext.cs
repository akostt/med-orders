using MedOrders.Web.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedOrders.Web.Persistence;

/// <summary>
/// Контекст базы данных MedOrders. Схема описана в классах Configurations,
/// учётные записи и демо-каталог — в HasData, поэтому они попадают в миграцию.
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, int>(options)
{
    /// <summary>Категории товаров.</summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>Производители.</summary>
    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();

    /// <summary>Склады.</summary>
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    /// <summary>Товары.</summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>Регистрационные документы товаров.</summary>
    public DbSet<Certificate> Certificates => Set<Certificate>();

    /// <summary>Остатки товаров по складам.</summary>
    public DbSet<Stock> Stocks => Set<Stock>();

    /// <summary>Профили клиентов.</summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>Заказы.</summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>Позиции заказов.</summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <summary>Журнал действий.</summary>
    public DbSet<ActionLog> ActionLogs => Set<ActionLog>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
