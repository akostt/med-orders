using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Manufacturer.</summary>
public sealed class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        builder.ToTable("Manufacturer");
        builder.Property(manufacturer => manufacturer.Id).HasColumnName("ID_Manufacturer");
        builder.Property(manufacturer => manufacturer.Name).HasMaxLength(200).IsRequired();
        builder.Property(manufacturer => manufacturer.Country).HasMaxLength(100).IsRequired();
        builder.HasData(CatalogSeedData.Manufacturers);
    }
}
