using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorTemperatura : Sensor
{
    private List<RegistroTemperatura> _registrosTemperatura;

    public SensorTemperatura(string codigo, Heladera heladera) : base(codigo, heladera)
    {
        _registrosTemperatura = new List<RegistroTemperatura>();
    }

    public void Registrar(DateTime fecha, float temperatura)
    {
        _registrosTemperatura.Add(new RegistroTemperatura(fecha, temperatura));
    }
}