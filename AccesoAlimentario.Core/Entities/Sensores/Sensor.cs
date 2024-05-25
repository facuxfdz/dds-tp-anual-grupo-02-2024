using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class Sensor
{
    private string _codigo;
    private Heladera _heladera;
    
    public Sensor(string codigo, Heladera heladera)
    {
        _codigo = codigo;
        _heladera = heladera;
    }
}