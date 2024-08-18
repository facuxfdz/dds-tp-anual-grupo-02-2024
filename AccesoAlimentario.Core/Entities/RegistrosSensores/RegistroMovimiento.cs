using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.RegistrosSensores;

public class RegistroFraude
{
    public Heladera Heladera { get; }
    public DateTime FechaLectura { get; }
    
    public RegistroFraude()
    {
    }
    
    public RegistroFraude(Heladera heladera, DateTime fechaLectura)
    {
        Heladera = heladera;
        FechaLectura = fechaLectura;
    }
}