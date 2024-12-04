using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorMovimiento : Sensor, ISubjectHeladeraMovimiento
{
    public virtual List<RegistroMovimiento> RegistrosMovimiento { get; set; } = [];
    private List<IObserverSensorMovimiento> Observadores { get; set; } = [];
    
    public SensorMovimiento()
    {
    }
    public override void Registrar(DateTime fecha, string movimiento)
    {
        try
        {
            RegistrosMovimiento.Add(new RegistroMovimiento(fecha, Convert.ToBoolean(movimiento)));
            Notificar(RegistrosMovimiento.Last().Movimiento, false);
        }
        catch (Exception e)
        {
            Notificar(false, true);
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void Suscribirse(IObserverSensorMovimiento observado)
    {
        Observadores.Add(observado);
    }
    public void Desuscribirse(IObserverSensorMovimiento observado)
    {
        Observadores.Remove(observado);
    }
    public void Notificar(bool dato, bool error)
    {
        if (Observadores.Count == 0)
        {
            Observadores.Add(Heladera);
        }
        foreach (var observador in Observadores)
        {
            observador.CambioSensorMovimiento(RegistrosMovimiento.Last().Movimiento, false);
        }
    }
}