public interface ISubjectHeladeraTemperatura
{//TODO
    void Suscribirse(IObserverSensorTemperatura observer);
    void Desuscribirse(IObserverSensorTemperatura observer);
    void Notificar();
}