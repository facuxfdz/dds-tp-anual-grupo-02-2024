using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public interface ISubjectHeladeraMovimiento
{
    void Suscribirse(IObserverSensorMovimiento observer);
    void Desuscribirse(IObserverSensorMovimiento observer);
    void Notificar(bool dato, bool error);
}