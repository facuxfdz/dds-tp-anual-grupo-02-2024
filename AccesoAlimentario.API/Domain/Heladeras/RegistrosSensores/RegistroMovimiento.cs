using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;

public class RegistroFraude : RegistroSensor
{
    
    public RegistroFraude()
    {
    }
    
    public RegistroFraude(Heladera heladera, DateTime fechaLectura) : base(heladera, fechaLectura)
    {
    }
}