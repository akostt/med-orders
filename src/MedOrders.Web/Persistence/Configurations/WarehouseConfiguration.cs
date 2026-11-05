using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Warehouse.</summary>
public sealed class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouse");
        builder.Property(warehouse => warehouse.Id).HasColumnName("ID_Warehouse");
        builder.Property(warehouse => warehouse.Name).HasMaxLength(150).IsRequired();
        builder.Property(warehouse => warehouse.Address).HasMaxLength(300).IsRequired();
        builder.HasData(CatalogSeedData.Warehouses);
    }
}
