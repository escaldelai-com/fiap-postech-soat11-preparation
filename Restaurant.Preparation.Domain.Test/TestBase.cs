using Bogus;

namespace Restaurant.Preparation.Domain.Test;

public abstract class TestBase
{

    protected Faker faker = new("pt_BR");

    protected string GetGuid() => Guid.NewGuid().ToString("n");

    protected OrderItem GetItem()
    {
        return new OrderItem(
            faker.Commerce.ProductName(),
            faker.Commerce.Categories(1).First()
        );
    }

}
