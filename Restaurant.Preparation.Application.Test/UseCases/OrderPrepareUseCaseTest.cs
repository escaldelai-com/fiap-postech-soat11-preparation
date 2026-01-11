using FluentAssertions;
using Moq;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.UseCases;
using Restaurant.Preparation.Domain;

namespace Restaurant.Preparation.Application.Test;

public class OrderPrepareUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderPrepareUseCase_Ok()
    {
        // Arrange
        var (repo, orderGet, orderCreate, useCase) = GetMocks();
        var order = GetOrderDto(3, OrderStatus.Received);
        orderGet.Setup(x => x.Get(order.Id!)).ReturnsAsync(order);
        orderCreate.Setup(x => x.Create(order)).Returns(GetOrder(order));
        repo.Setup(x => x.Update(order)).ReturnsAsync(order);

        // Act
        var result = await useCase.Prepare(order.Id!);

        // Assert
        result.Status.Should().Be(OrderStatus.Preparing);
    }

    [Fact]
    public async Task OrderPrepareUseCase_Null_Order()
    {
        // Arrange
        var (_, _, _, useCase) = GetMocks();

        // Act
        var act = () => useCase.Prepare(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }



    private (Mock<IOrderRepository>, Mock<IOrderGetService>, Mock<IOrderCreateService>, OrderPrepareUseCase) GetMocks()
    {
        var repo = new Mock<IOrderRepository>();
        var orderGet = new Mock<IOrderGetService>();
        var orderCreate = new Mock<IOrderCreateService>();
        var useCase = new OrderPrepareUseCase(repo.Object, orderGet.Object, orderCreate.Object);

        return (repo, orderGet, orderCreate, useCase);
    }

}
