using MedOrders.Web.Domain;
using MedOrders.Web.Persistence;
using MedOrders.Web.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MedOrders.Tests;

/// <summary>Сохранение товара менеджером: товар, документ и остаток пишутся вместе.</summary>
public class ProductServiceTests : IDisposable
{
    private const string TestArticle = "TEST-0001";

    private static AppDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=MedOrders;User Id=sa;Password=ALEX1219!;TrustServerCertificate=True")
            .Options);

    [Fact]
    public async Task CreateProductAsync_ValidCommand_PersistsProductCertificateAndStock()
    {
        var command = new CreateProductCommand(
            TestArticle, "Тестовый тонометр", 1, 2, 1990m, 12, 24,
            "+5…+40 °C", 5, CertificateType.RegistrationCertificate, "РУ-Т-0001",
            new DateOnly(2026, 1, 15));

        await using var db = CreateContext();
        var service = new ProductService(db, TimeProvider.System);

        var article = await service.CreateProductAsync(command, 2);

        Assert.Equal(TestArticle, article);
        var product = await db.Products.Include(p => p.Certificates).Include(p => p.Stocks)
            .SingleAsync(p => p.Article == TestArticle);
        var productId = product.Id;
        Assert.Equal(1990m, product.Price);
        Assert.Equal("РУ-Т-0001", product.Certificates.Single().Number);
        Assert.Equal(5, product.Stocks.Single().Quantity);
    }

    public void Dispose()
    {
        using var db = CreateContext();
        var product = db.Products.Include(p => p.Certificates).Include(p => p.Stocks)
            .SingleOrDefault(p => p.Article == TestArticle);
        if (product is not null)
        {
            var logs = db.ActionLogs.Where(l => l.TableName == "Product" && l.RecordId == product.Id);
            db.ActionLogs.RemoveRange(logs);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}
