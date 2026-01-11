namespace Restaurant.Preparation.Domain;

public class OrderItem : IComparable<OrderItem>
{

    public string Nome { get; private set; }

    public string Tipo { get; private set; }


    public OrderItem(string nome, string tipo)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(nome)
            .IsNotNullOrWhiteSpace(tipo)
            .Validate();

        Nome = nome;
        Tipo = tipo;
    }


    public int CompareTo(OrderItem? other)
    {
        if (other == null) return 1;

        var result = Tipo.CompareTo(other.Tipo);
        if (result != 0) return result;

        result = Nome.CompareTo(other.Nome);
        if (result != 0) return result;

        return result;
    }

}
