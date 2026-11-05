using MedOrders.Web.Services;
using Xunit;

namespace MedOrders.Tests;

/// <summary>Проверки расчёта итога заказа (скидка, НДС, доставка).</summary>
public class OrderTotalsCalculatorTests
{
    [Fact]
    public void Calculate_WithoutDiscount_ReturnsExpectedTotals()
    {
        var totals = OrderTotalsCalculator.Calculate(4590m, 1, 0m, 22m, 1500m);

        Assert.Equal(4590m, totals.Subtotal);
        Assert.Equal(0m, totals.Discount);
        Assert.Equal(1009.80m, totals.Vat);
        Assert.Equal(1500m, totals.DeliveryCost);
        Assert.Equal(7099.80m, totals.Total);
    }

    [Fact]
    public void Calculate_WithQuantity2_AppliesVatToSubtotal()
    {
        var totals = OrderTotalsCalculator.Calculate(4590m, 2, 0m, 22m, 1500m);

        Assert.Equal(9180m, totals.Subtotal);
        Assert.Equal(2019.60m, totals.Vat);
        Assert.Equal(12699.60m, totals.Total);
    }

    [Fact]
    public void Calculate_PickupDelivery_AddsNothing()
    {
        var totals = OrderTotalsCalculator.Calculate(4590m, 1, 0m, 22m, 0m);

        Assert.Equal(5599.80m, totals.Total);
    }

    [Fact]
    public void Calculate_WithDiscount_ReducesVatBase()
    {
        var totals = OrderTotalsCalculator.Calculate(1000m, 1, 10m, 22m, 0m);

        Assert.Equal(100m, totals.Discount);
        Assert.Equal(198m, totals.Vat);
        Assert.Equal(1098m, totals.Total);
    }
}
