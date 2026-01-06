using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.ExternalServices;

public interface IIdentificationService
{

    Task<ClientDto?> GetById(string? id);

    Task<IEnumerable<ClientDto>> Get(IEnumerable<string> ids);

}
