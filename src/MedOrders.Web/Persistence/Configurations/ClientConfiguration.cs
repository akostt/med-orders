using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Client: один профиль на одну учётную запись.</summary>
public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");
        builder.Property(client => client.Id).HasColumnName("ID_Client");
        builder.Property(client => client.UserId).HasColumnName("ID_User");
        builder.Property(client => client.ClientType).HasConversion<string>().HasMaxLength(30);
        builder.Property(client => client.Address).HasMaxLength(300).IsRequired();
        builder.HasIndex(client => client.UserId).IsUnique();
        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(client => client.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasData(AccountSeedData.Clients);
    }
}
