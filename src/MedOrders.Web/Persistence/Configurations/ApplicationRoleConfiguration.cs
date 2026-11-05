using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Role и роли по умолчанию.</summary>
public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Role");
        builder.Property(role => role.Id).HasColumnName("ID_Role");
        builder.Property(role => role.Name).HasColumnName("Name").HasMaxLength(50);
        builder.Property(role => role.AccessRights).HasMaxLength(500).IsRequired();
        builder.HasData(AccountSeedData.Roles);
    }
}
