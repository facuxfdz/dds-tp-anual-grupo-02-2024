using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorTemperatura : Sensor, ISubjectHeladeraTemperatura
{
    public List<RegistroTemperatura> RegistrosTemperatura { get; set; } = [];

    public SensorTemperatura()
    {
    }
    
    public override void Registrar(DateTime fecha, string temperatura)
    {
        RegistrosTemperatura.Add(new RegistroTemperatura(fecha, Convert.ToSingle(temperatura)));
    }
    
    public void Suscribirse(IObserverSensorTemperatura observado)
    {
        //TODO
    }
    public void Desuscribirse(IObserverSensorTemperatura observado)
    {
        //TODO
    }
    public void Notificar()
    {
        //TODO
    }
}