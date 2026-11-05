using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы ActionLog.</summary>
public sealed class ActionLogConfiguration : IEntityTypeConfiguration<ActionLog>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ActionLog> builder)
    {
        builder.ToTable("ActionLog");
        builder.Property(log => log.Id).HasColumnName("ID_ActionLog");
        builder.Property(log => log.UserId).HasColumnName("ID_User");
        builder.Property(log => log.Action).HasMaxLength(50).IsRequired();
        builder.Property(log => log.TableName).HasMaxLength(50).IsRequired();
        builder.Property(log => log.RecordId).HasColumnName("RecordID");
    }
}
