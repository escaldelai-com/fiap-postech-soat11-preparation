using FluentAssertions;
using Restaurant.Preparation.Application.Services;

namespace Restaurant.Preparation.Application.Test;

public class OrderCreateServiceTest : TestBase
{

    [Fact]
    public void OrderCreateService_Ok()
    {
        // Arrange
        var data = GetOrderDto();
        var service = new OrderCreateService();

        // Act
        var result = service.Create(data);

        // Assert
        result.Should().NotBeNull();
    }

}
