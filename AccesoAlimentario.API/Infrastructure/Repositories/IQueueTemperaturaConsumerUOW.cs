using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;

namespace AccesoAlimentario.API.Infrastructure.Repositories;

public interface IQueueTemperaturaConsumerUOW
{
    IRepository<Heladera> HeladeraRepository { get; }
    IRepository<RegistroTemperatura> RegistroTemperaturaRepository { get; }
    void Complete();
}