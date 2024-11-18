using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorTemperatura : Sensor, ISubjectHeladeraTemperatura
{
    public virtual List<RegistroTemperatura> RegistrosTemperatura { get; set; } = [];
    private List<IObserverSensorTemperatura> Observadores { get; set; } = [];

    public SensorTemperatura()
    {
    }

    public override void Registrar(DateTime fecha, string temperatura)
    {
        try
        {
            RegistrosTemperatura.Add(new RegistroTemperatura(fecha, Convert.ToSingle(temperatura)));
            Notificar(Convert.ToSingle(temperatura), false);
        }
        catch (Exception e)
        {
            Notificar(0, true);
        }
    }

    public void Suscribirse(IObserverSensorTemperatura observado)
    {
        Observadores.Add(observado);
    }

    public void Desuscribirse(IObserverSensorTemperatura observado)
    {
        Observadores.Remove(observado);
    }

    public void Notificar(float dato, bool error)
    {
        foreach (var observador in Observadores)
        {
            observador.CambioSensorTemperatura(dato, error);
        }
    }
}