namespace Restaurant.Preparation.Domain;

public class Order
{

    public DateTime Data { get; private set; }

    public int Numero { get; private set; }

    public string Cliente { get; private set; }

    public string Status { get; private set; }

    public OrderItem[] Items { get; private set; }

    public Order(DateTime data, int numero, string cliente, string status, OrderItem[] items)
    {
        Validator.Create()
            .IsInThePastOrPresent(data)
            .GreaterThanZero(numero)
            .IsNotNullOrWhiteSpace(cliente)
            .IsNotNullOrWhiteSpace(status)
            .IsNotNull(items)
            .GreaterThanZero(items?.Length ?? 0)
            .Validate();

        Data = data;
        Numero = numero;
        Cliente = cliente;
        Status = status;
        Items = items!;
    }


    public void Confirm()
    {
        if (Status != OrderStatus.Paid)
            throw new OrderStatusException(Status, "confirm");

        Status = OrderStatus.Received;
    }

    public void Prepare()
    {
        if (Status != OrderStatus.Received)
            throw new OrderStatusException(Status, "preparing");

        Status = OrderStatus.Preparing;
    }

    public void Delivery()
    {
        if (Status != OrderStatus.Preparing)
            throw new OrderStatusException(Status, "delivering");

        Status = OrderStatus.Delivery;
    }

    public void OrderFinalize()
    {
        if (Status != OrderStatus.Delivery)
            throw new OrderStatusException(Status, "finalizing");

        Status = OrderStatus.Done;
    }

}
