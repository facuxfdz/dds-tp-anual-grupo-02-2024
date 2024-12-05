using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorTemperatura : Sensor, ISubjectHeladeraTemperatura
{
    public virtual List<RegistroTemperatura> RegistrosTemperatura { get; set; } = [];
    private List<IObserverSensorTemperatura> Observadores { get; set; } = [];

    public SensorTemperatura()
    {
    }

    public override Guid Registrar(DateTime fecha, string temperatura)
    {
        try
        {
            var registro = new RegistroTemperatura(fecha, Convert.ToSingle(temperatura));
            RegistrosTemperatura.Add(registro);
            Notificar(Convert.ToSingle(temperatura), false);
            return registro.Id;
        }
        catch (Exception e)
        {
            Notificar(0, true);
            Console.WriteLine(e);
            return Guid.Empty;
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
        if (Observadores.Count == 0)
        {
            Observadores.Add(Heladera);
        }
        foreach (var observador in Observadores)
        {
            observador.CambioSensorTemperatura(dato, error);
        }
    }
}