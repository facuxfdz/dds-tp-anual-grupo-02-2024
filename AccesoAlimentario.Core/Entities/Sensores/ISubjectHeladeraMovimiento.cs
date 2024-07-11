public interface ISubjectHeladeraMovimiento
{//TODO
    void Suscribirse(IObserverSensorMovimiento observer);
    void Desuscribirse(IObserverSensorMovimiento observer);
    void Notificar();
}