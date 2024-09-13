using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;

namespace AccesoAlimentario.API.Infrastructure.Repositories;

public interface IQueueFraudeConsumerUOW
{
    IRepository<Heladera> HeladeraRepository { get; }
    IRepository<RegistroFraude> RegistroFraudeRepository { get; }
    void Complete();
}