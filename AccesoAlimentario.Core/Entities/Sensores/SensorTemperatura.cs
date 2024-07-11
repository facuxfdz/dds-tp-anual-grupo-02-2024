using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class SensorTemperatura : ISensor, ISubjectHeladeraTemperatura
{
    private List<RegistroTemperatura> _registrosTemperatura;

    public SensorTemperatura()
    {
        _registrosTemperatura = new List<RegistroTemperatura>();
    }

    public void Registrar(DateTime fecha, float temperatura)
    {
        _registrosTemperatura.Add(new RegistroTemperatura(fecha, temperatura));
    }
    //TODO
    public void Suscribirse(IObserverSensorTemperatura observado)
    public void Desuscribirse(IObserverSensorTemperatura observado)
    public void Notificar()
}