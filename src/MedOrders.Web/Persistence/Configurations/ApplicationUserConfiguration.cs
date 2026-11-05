using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы User и учётные записи по умолчанию.</summary>
public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("User");
        builder.Property(user => user.Id).HasColumnName("ID_User");
        builder.Property(user => user.UserName).HasColumnName("Login").HasMaxLength(50).IsRequired();
        builder.Property(user => user.Email).HasColumnName("Email").HasMaxLength(100);
        builder.Property(user => user.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(255);
        builder.Property(user => user.FullName).HasMaxLength(150).IsRequired();
        builder.Property(user => user.RoleId).HasColumnName("ID_Role");
        builder.HasOne(user => user.Role).WithMany().HasForeignKey(user => user.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasData(AccountSeedData.Users);
    }
}
