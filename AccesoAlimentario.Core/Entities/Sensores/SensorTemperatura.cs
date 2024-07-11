using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorTemperatura : ISensor, ISubjectHeladeraTemperatura
{
    private List<RegistroTemperatura> _registrosTemperatura;

    public SensorTemperatura()
    {
        _registrosTemperatura = new List<RegistroTemperatura>();
    }

    public void Registrar(DateTime fecha, string temperatura)
    {
        _registrosTemperatura.Add(new RegistroTemperatura(fecha, Convert.ToSingle(temperatura)));
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