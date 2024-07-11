using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorMovimiento : ISensor, ISubjectHeladeraMovimiento
{
    private readonly List<RegistroMovimiento> _registrosMovimiento;

    public SensorMovimiento(string codigo, Heladera heladera)
    {
        _registrosMovimiento = new List<RegistroMovimiento>();
    }
    
    public void Registrar(DateTime fecha, bool movimiento)
    {
        _registrosMovimiento.Add(new RegistroMovimiento(fecha, movimiento));
    }
    // TODO
    public void Suscribirse(IobserverSensorMovimiento observado());
    public void Desuscribirse(IobserverSensorMovimiento observado());
    public void Notificar();
}