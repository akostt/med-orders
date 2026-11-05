using MedOrders.Web.Services;
using Xunit;

namespace MedOrders.Tests;

/// <summary>Проверки разбора строки характеристик товара.</summary>
public class ProductSpecificationParserTests
{
    [Fact]
    public void Parse_ValidText_ReturnsRowsInOrder()
    {
        var rows = ProductSpecificationParser.Parse("Тип: Автоматический тонометр\nДиапазон: 0–299 мм");

        Assert.Equal(2, rows.Count);
        Assert.Equal("Тип", rows[0].Name);
        Assert.Equal("Автоматический тонометр", rows[0].Value);
        Assert.Equal("Диапазон", rows[1].Name);
    }

    [Fact]
    public void Parse_EmptyText_ReturnsEmptyList()
    {
        Assert.Empty(ProductSpecificationParser.Parse(""));
        Assert.Empty(ProductSpecificationParser.Parse(null));
    }
}
