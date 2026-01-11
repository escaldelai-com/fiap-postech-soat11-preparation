using FluentAssertions;
using Moq;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.UseCases;
using Restaurant.Preparation.Domain;

namespace Restaurant.Preparation.Application.Test;

public class OrderFinalizeUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderFinalizeUseCase_Ok()
    {
        // Arrange
        var (repo, orderGet, orderCreate, useCase) = GetMocks();
        var order = GetOrderDto(3, OrderStatus.Delivery);
        orderGet.Setup(x => x.Get(order.Id!)).ReturnsAsync(order);
        orderCreate.Setup(x => x.Create(order)).Returns(GetOrder(order));
        repo.Setup(x => x.Update(order)).ReturnsAsync(order);

        // Act
        var result = await useCase.Finalize(order.Id!);

        // Assert
        result.Status.Should().Be(OrderStatus.Done);
    }

    [Fact]
    public async Task OrderFinalizeUseCase_Null_Order()
    {
        // Arrange
        var (_, _, _, useCase) = GetMocks();

        // Act
        var act = () => useCase.Finalize(null!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }



    private (Mock<IOrderRepository>, Mock<IOrderGetService>, Mock<IOrderCreateService>, OrderFinalizeUseCase) GetMocks()
    {
        var repo = new Mock<IOrderRepository>();
        var orderGet = new Mock<IOrderGetService>();
        var orderCreate = new Mock<IOrderCreateService>();
        var useCase = new OrderFinalizeUseCase(repo.Object, orderGet.Object, orderCreate.Object);

        return (repo, orderGet, orderCreate, useCase);
    }

}
