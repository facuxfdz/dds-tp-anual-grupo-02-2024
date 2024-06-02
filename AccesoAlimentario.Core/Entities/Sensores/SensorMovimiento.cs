using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorMovimiento : Sensor
{
    private readonly List<RegistroMovimiento> _registrosMovimiento;

    public SensorMovimiento(string codigo, Heladera heladera) : base(codigo, heladera)
    {
        _registrosMovimiento = new List<RegistroMovimiento>();
    }
    
    public void Registrar(DateTime fecha, bool movimiento)
    {
        _registrosMovimiento.Add(new RegistroMovimiento(fecha, movimiento));
    }
}