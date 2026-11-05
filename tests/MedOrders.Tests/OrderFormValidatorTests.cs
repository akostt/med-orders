using MedOrders.Web.Domain;
using MedOrders.Web.Services;
using Xunit;

namespace MedOrders.Tests;

/// <summary>Проверки формы оформления заказа.</summary>
public class OrderFormValidatorTests
{
    private static CreateOrderCommand Command(
        string address = "Владимир, ул. Горького, д. 58",
        int quantity = 1) =>
        new(3, 4, quantity, DeliveryMethod.Courier, address, null);

    [Fact]
    public void Validate_CorrectForm_ReturnsNoErrors()
    {
        Assert.Empty(OrderFormValidator.Validate(Command()));
    }

    [Fact]
    public void Validate_EmptyAddress_ReturnsRequiredError()
    {
        var errors = OrderFormValidator.Validate(Command(address: "   "));
        Assert.True(errors.ContainsKey("DeliveryAddress"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void Validate_NonPositiveQuantity_ReturnsError(int quantity)
    {
        var errors = OrderFormValidator.Validate(Command(quantity: quantity));
        Assert.True(errors.ContainsKey("Quantity"));
    }

    [Fact]
    public void Validate_TooLongAddress_ReturnsError()
    {
        var errors = OrderFormValidator.Validate(Command(address: new string('а', 301)));
        Assert.Contains("не длиннее", errors["DeliveryAddress"]);
    }
}
