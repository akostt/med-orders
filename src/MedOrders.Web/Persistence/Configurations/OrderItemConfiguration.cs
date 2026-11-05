using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы OrderItem.</summary>
public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");
        builder.Property(item => item.Id).HasColumnName("ID_OrderItem");
        builder.Property(item => item.OrderId).HasColumnName("ID_Order");
        builder.Property(item => item.ProductId).HasColumnName("ID_Product");
        builder.Property(item => item.WarehouseId).HasColumnName("ID_Warehouse");
        builder.Property(item => item.UnitPrice).HasPrecision(12, 2);
        builder.Property(item => item.LineTotal).HasPrecision(12, 2);
        builder.HasOne(item => item.Product).WithMany().HasForeignKey(item => item.ProductId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Warehouse>().WithMany().HasForeignKey(item => item.WarehouseId).OnDelete(DeleteBehavior.Restrict);
    }
}
