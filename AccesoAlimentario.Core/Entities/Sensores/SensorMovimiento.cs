using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorMovimiento : Sensor, ISubjectHeladeraMovimiento
{
    public virtual List<RegistroMovimiento> RegistrosMovimiento { get; set; } = [];
    private List<IObserverSensorMovimiento> Observadores { get; set; } = [];
    
    public SensorMovimiento()
    {
    }
    public override Guid Registrar(DateTime fecha, string movimiento)
    {
        try
        {
            var registro = new RegistroMovimiento(fecha, Convert.ToBoolean(movimiento));
            RegistrosMovimiento.Add(registro);
            Notificar(RegistrosMovimiento.Last().Movimiento, false);
            return registro.Id;
        }
        catch (Exception e)
        {
            Notificar(false, true);
            Console.WriteLine(e);
            return Guid.Empty;
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