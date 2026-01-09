using FluentAssertions;
using Moq;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.UseCases;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Application.Test;

public class OrderConfirmUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderConfirmUseCase_Ok()
    {
        // Arrange
        var (repo, orderCreate, useCase) = GetMocks();
        var order = GetOrderDto();
        orderCreate.Setup(x => x.Create(order)).Returns(GetOrder(order));
        repo.Setup(x => x.Create(order)).ReturnsAsync(order);

        // Act
        var result = await useCase.Confirm(order!);

        // Assert
        result.Status.Should().Be(OrderStatus.Received);
    }

    [Fact]
    public async Task OrderConfirmUseCase_Null_Order()
    {
        // Arrange
        var (_, _, useCase) = GetMocks();

        // Act
        var act = () => useCase.Confirm(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }



    private (Mock<IOrderRepository>, Mock<IOrderCreateService>, OrderConfirmUseCase) GetMocks()
    {
        var repo = new Mock<IOrderRepository>();
        var orderCreate = new Mock<IOrderCreateService>();
        var useCase = new OrderConfirmUseCase(repo.Object, orderCreate.Object);

        return (repo, orderCreate, useCase);
    }

}
