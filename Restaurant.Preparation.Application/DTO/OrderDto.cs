using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Application.DTO;

public class OrderDto
{

    public string? Id { get; set; }

    public DateTime? Data { get; set; }

    public int? Numero { get; set; }

    public ClientDto? Cliente { get; set; }

    public string? Status { get; set; }

    public OrderItemDto[] Items { get; set; } = [];

}
