using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.RegistrosSensores;

public class RegistroTemperatura
{
    public Heladera Heladera { get; }
    public DateTime FechaLectura { get; }
    public float Temperatura { get; } // En grados Celsius
    
    public RegistroTemperatura()
    {
    }
    public RegistroTemperatura(Heladera heladera, DateTime fechaLectura, float temperatura)
    {
        Heladera = heladera;
        FechaLectura = fechaLectura;
        Temperatura = temperatura;
    }
    
    
}