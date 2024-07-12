using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public interface ISubjectHeladeraMovimiento
{//TODO
    void Suscribirse(IObserverSensorMovimiento observer);
    void Desuscribirse(IObserverSensorMovimiento observer);
    void Notificar();
}