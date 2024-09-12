using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;
using AccesoAlimentario.API.Infrastructure.Repositories;

namespace AccesoAlimentario.API.UseCases.RegistrarDataHeladera;

public class RegistrarTemperaturaHeladera : IRegistrarEventoHeladera
{
    private GenericRepository<RegistroTemperatura> _registroTemperaturaRepository;
    public RegistrarTemperaturaHeladera(GenericRepository<RegistroTemperatura> registroTemperaturaRepository)
    {
        _registroTemperaturaRepository = registroTemperaturaRepository;
    }
    public void RegistrarEvento(string message)
    {
        Console.WriteLine("RegistrarTemperaturaHeladera: " + message);
    }
}