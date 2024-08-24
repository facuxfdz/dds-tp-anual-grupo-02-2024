using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;

public class RegistroTemperatura : RegistroSensor
{
    public float Temperatura { get; } // En grados Celsius
    
    public RegistroTemperatura()
    {
    }
    public RegistroTemperatura(Heladera heladera, DateTime fechaLectura, float temperatura) : base(heladera, fechaLectura)
    {
        Temperatura = temperatura;
    }
    
    
}