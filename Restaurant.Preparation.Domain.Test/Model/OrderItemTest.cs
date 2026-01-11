using Bogus;
using FluentAssertions;

namespace Restaurant.Preparation.Domain.Test;

public class OrderItemTest : TestBase
{

    [Fact]
    public void OrderItem_Ok()
    {
        // Arrange
        var item = new
        {
            Nome = faker.Commerce.ProductName(),
            Tipo = faker.Commerce.Categories(1).First(),
        };

        // Act
        var test = new OrderItem(
            item.Nome,
            item.Tipo
        );

        // Assert
        test.Should().BeEquivalentTo(item);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderItem_Invalid_Name(string? value)
    {
        // Act
        var act = () => new OrderItem(
            value!,
            faker.Commerce.Categories(1).First()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderItem_Invalid_Type(string? value)
    {
        // Act
        var act = () => new OrderItem(
            faker.Commerce.ProductName(),
            value!
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void OrderItem_CompareTo_Equals()
    {
        // Arrange
        var item = new
        {
            Nome = faker.Commerce.ProductName(),
            Tipo = faker.Commerce.Categories(1).First(),
        };
        var orderItem1 = new OrderItem(item.Nome, item.Tipo);
        var orderItem2 = new OrderItem(item.Nome, item.Tipo);

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(0);
    }

    [Fact]
    public void OrderItem_CompareTo_Null()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "A");
        OrderItem? orderItem2 = null;

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(1);
    }

    [Fact]
    public void OrderItem_CompareTo_Type_Greater()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "B");
        var orderItem2 = new OrderItem("A", "A");

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(1);
    }

    [Fact]
    public void OrderItem_CompareTo_Type_Less()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "A");
        var orderItem2 = new OrderItem("A", "B");

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(-1);
    }

    [Fact]
    public void OrderItem_CompareTo_Name_Greater()
    {
        // Arrange
        var orderItem1 = new OrderItem("B", "A");
        var orderItem2 = new OrderItem("A", "A");

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(1);
    }

    [Fact]
    public void OrderItem_CompareTo_Name_Less()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "A");
        var orderItem2 = new OrderItem("B", "A");

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(-1);
    }

}
