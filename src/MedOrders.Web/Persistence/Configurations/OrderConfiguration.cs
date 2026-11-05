using MedOrders.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedOrders.Web.Persistence.Configurations;

/// <summary>Схема таблицы Order: уникальный номер, статусы строкой, денежные поля с двумя знаками.</summary>
public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");
        builder.Property(order => order.Id).HasColumnName("ID_Order");
        builder.Property(order => order.Number).HasMaxLength(30).IsRequired();
        builder.HasIndex(order => order.Number).IsUnique();
        builder.Property(order => order.ClientId).HasColumnName("ID_Client");
        builder.Property(order => order.Status).HasConversion<string>().HasMaxLength(30);
        builder.Property(order => order.DeliveryMethod).HasConversion<string>().HasMaxLength(30);
        builder.Property(order => order.PaymentStatus).HasConversion<string>().HasMaxLength(20);
        builder.Property(order => order.DeliveryAddress).HasMaxLength(300).IsRequired();
        builder.Property(order => order.DeliveryCost).HasPrecision(12, 2);
        builder.Property(order => order.Discount).HasPrecision(12, 2);
        builder.Property(order => order.DiscountPercent).HasPrecision(5, 2);
        builder.Property(order => order.TaxAmount).HasPrecision(12, 2);
        builder.Property(order => order.TotalAmount).HasPrecision(12, 2);
        builder.Property(order => order.Notes).HasMaxLength(500);
        builder.HasOne<Client>().WithMany().HasForeignKey(order => order.ClientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(order => order.Items).WithOne().HasForeignKey(item => item.OrderId).OnDelete(DeleteBehavior.Cascade);
    }
}
