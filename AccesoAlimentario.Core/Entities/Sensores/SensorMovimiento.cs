using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorMovimiento : Sensor, ISubjectHeladeraMovimiento
{
    public List<RegistroMovimiento> RegistrosMovimiento { get; set; } = [];
    
    public SensorMovimiento()
    {
    }
    
    public override void Registrar(DateTime fecha, string movimiento)
    {
        RegistrosMovimiento.Add(new RegistroMovimiento(fecha, Convert.ToBoolean(movimiento)));
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