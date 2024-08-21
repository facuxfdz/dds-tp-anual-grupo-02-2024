using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.RegistrosSensores;

namespace AccesoAlimentario.API.Controllers;

public class RegistrarTemperaturaHeladera
{
    private GenericRepository<RegistroTemperatura> _registroTemperaturaRepository;
    public RegistrarTemperaturaHeladera(GenericRepository<RegistroTemperatura> registroTemperaturaRepository)
    {
        _registroTemperaturaRepository = registroTemperaturaRepository;
    }
    public void RegistrarTemperatura(RegistroTemperatura registroTemperatura)
    {
        _registroTemperaturaRepository.Insert(registroTemperatura);
    }
}