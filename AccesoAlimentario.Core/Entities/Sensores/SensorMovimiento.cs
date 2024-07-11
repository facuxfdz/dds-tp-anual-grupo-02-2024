using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorMovimiento : ISensor, ISubjectHeladeraMovimiento
{
    private readonly List<RegistroMovimiento> _registrosMovimiento;

    public SensorMovimiento(string codigo, Heladera heladera)
    {
        _registrosMovimiento = new List<RegistroMovimiento>();
    }
    
    public void Registrar(DateTime fecha, string movimiento)
    {
        _registrosMovimiento.Add(new RegistroMovimiento(fecha, Convert.ToBoolean(movimiento)));
    }
    
    public void Suscribirse(IObserverSensorMovimiento observado)
    {
        //TODO
    }
    public void Desuscribirse(IObserverSensorMovimiento observado)
    {
        //TODO
    }
    public void Notificar()
    {
        //TODO
    }
}