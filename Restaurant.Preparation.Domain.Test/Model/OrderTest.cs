using FluentAssertions;

namespace Restaurant.Preparation.Domain.Test;

public class OrderTest : TestBase
{

    [Fact]
    public void Order_Ok()
    {
        // Arrange
        var data = new
        {
            Data = faker.Date.Past(),
            Numero = faker.Random.Int(1, 9999),
            Cliente = GetGuid(),
            Status = faker.Random.Word(),
            Items = new[]
            {
                new
                {
                    Nome = faker.Commerce.ProductName(),
                    Tipo = faker.Commerce.Categories(1).First()
                }
            }
        };

        // Act
        var order = new Order(
            data.Data, data.Numero, data.Cliente, data.Status,
            data.Items.Select(i => new OrderItem(i.Nome, i.Tipo)).ToArray()
        );

        // Assert
        order.Should().BeEquivalentTo(data);
    }

    [Fact]
    public void Order_Invalid_Date()
    {
        // Act
        var act = () => new Order(
            faker.Date.Future(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            faker.Random.Word(),
            faker.Make(1, GetItem).ToArray()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Order_Invalid_Number(int value)
    {
        // Act
        var act = () => new Order(
            faker.Date.Past(),
            value,
            GetGuid(),
            faker.Random.Word(),
            faker.Make(1, GetItem).ToArray()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Order_Invalid_Client(string? value)
    {
        // Act
        var act = () => new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            value!,
            faker.Random.Word(),
            faker.Make(1, GetItem).ToArray()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Order_Invalid_Status(string? value)
    {
        // Act
        var act = () => new Order(
            faker.Date.Future(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            value!,
            faker.Make(1, GetItem).ToArray()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Order_Null_Items()
    {
        // Act
        var act = () => new Order(
            faker.Date.Future(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            faker.Random.Word(),
            null!
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Order_Empty_Items()
    {
        // Act
        var act = () => new Order(
            faker.Date.Future(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            faker.Random.Word(),
            Array.Empty<OrderItem>()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Order_Confirm_Ok()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Paid,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        order.Confirm();

        // Assert
        order.Status.Should().Be(OrderStatus.Received);
    }

    [Fact]
    public void Order_Confirm_Invalid_Status()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Canceled,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        var act = order.Confirm;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

    [Fact]
    public void Order_Prepare_Ok()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Received,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        order.Prepare();

        // Assert
        order.Status.Should().Be(OrderStatus.Preparing);
    }

    [Fact]
    public void Order_Prepare_Invalid_Status()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Canceled,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        var act = order.Prepare;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

    [Fact]
    public void Order_Delivery_Ok()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Preparing,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        order.Delivery();

        // Assert
        order.Status.Should().Be(OrderStatus.Delivery);
    }

    [Fact]
    public void Order_Delivery_Invalid_Status()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Canceled,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        var act = order.Delivery;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

    [Fact]
    public void Order_Finalize_Ok()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Delivery,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        order.OrderFinalize();

        // Assert
        order.Status.Should().Be(OrderStatus.Done);
    }

    [Fact]
    public void Order_Finalize_Invalid_Status()
    {
        // Arrange
        var order = new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Canceled,
            [
                new OrderItem(
                    faker.Commerce.ProductName(),
                    faker.Commerce.Categories(1).First())
            ]
        );

        // Act
        var act = order.OrderFinalize;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

}
