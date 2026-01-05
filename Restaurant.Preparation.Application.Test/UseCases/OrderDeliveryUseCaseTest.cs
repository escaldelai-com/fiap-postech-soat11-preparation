using FluentAssertions;
using Moq;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.UseCases;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Application.Test;

public class OrderDeliveryUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderDeliveryUseCase_Ok()
    {
        // Arrange
        var (repo, orderGet, orderCreate, useCase) = GetMocks();
        var order = GetOrderDto(3, OrderStatus.Preparing);
        orderGet.Setup(x => x.Get(order.Id!)).ReturnsAsync(order);
        orderCreate.Setup(x => x.Create(order)).Returns(GetOrder(order));
        repo.Setup(x => x.Update(order)).ReturnsAsync(order);

        // Act
        var result = await useCase.Delivery(order.Id!);

        // Assert
        result.Status.Should().Be(OrderStatus.Delivery);
    }

    [Fact]
    public async Task OrderDeliveryUseCase_Null_Order()
    {
        // Arrange
        var (_, _, _, useCase) = GetMocks();

        // Act
        var act = () => useCase.Delivery(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }



    private (Mock<IOrderRepository>, Mock<IOrderGetService>, Mock<IOrderCreateService>, OrderDeliveryUseCase) GetMocks()
    {
        var repo = new Mock<IOrderRepository>();
        var orderGet = new Mock<IOrderGetService>();
        var orderCreate = new Mock<IOrderCreateService>();
        var useCase = new OrderDeliveryUseCase(repo.Object, orderGet.Object, orderCreate.Object);

        return (repo, orderGet, orderCreate, useCase);
    }

}
