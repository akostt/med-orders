using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Product: уникальный артикул, связи с категорией, производителем, документами и остатками.</summary>
public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.Property(product => product.Id).HasColumnName("ID_Product");
        builder.Property(product => product.Article).HasMaxLength(50).IsRequired();
        builder.HasIndex(product => product.Article).IsUnique();
        builder.Property(product => product.Name).HasMaxLength(250).IsRequired();
        builder.Property(product => product.CategoryId).HasColumnName("ID_Category");
        builder.Property(product => product.ManufacturerId).HasColumnName("ID_Manufacturer");
        builder.Property(product => product.Price).HasPrecision(12, 2);
        builder.Property(product => product.RegistrationNumber).HasMaxLength(50);
        builder.Property(product => product.StorageConditions).HasMaxLength(250);
        builder.HasOne(product => product.Category).WithMany().HasForeignKey(product => product.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(product => product.Manufacturer).WithMany().HasForeignKey(product => product.ManufacturerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(product => product.Certificates).WithOne().HasForeignKey(certificate => certificate.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(product => product.Stocks).WithOne().HasForeignKey(stock => stock.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.HasData(CatalogSeedData.Products);
    }
}
