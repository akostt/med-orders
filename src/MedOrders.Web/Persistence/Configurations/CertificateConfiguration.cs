using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Certificate.</summary>
public sealed class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.ToTable("Certificate");
        builder.Property(certificate => certificate.Id).HasColumnName("ID_Certificate");
        builder.Property(certificate => certificate.ProductId).HasColumnName("ID_Product");
        builder.Property(certificate => certificate.DocType).HasConversion<string>().HasMaxLength(40);
        builder.Property(certificate => certificate.Number).HasMaxLength(50).IsRequired();
        builder.HasData(CatalogSeedData.Certificates);
    }
}
