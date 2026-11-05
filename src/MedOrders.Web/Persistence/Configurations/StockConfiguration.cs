using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Stock: один остаток на пару «товар–склад».</summary>
public sealed class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stock");
        builder.Property(stock => stock.Id).HasColumnName("ID_Stock");
        builder.Property(stock => stock.ProductId).HasColumnName("ID_Product");
        builder.Property(stock => stock.WarehouseId).HasColumnName("ID_Warehouse");
        builder.Ignore(stock => stock.Available);
        builder.HasIndex(stock => new { stock.ProductId, stock.WarehouseId }).IsUnique();
        builder.HasOne<Warehouse>().WithMany().HasForeignKey(stock => stock.WarehouseId).OnDelete(DeleteBehavior.Restrict);
        builder.HasData(CatalogSeedData.Stocks);
    }
}
