using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public interface ISubjectHeladeraTemperatura
{//TODO
    void Suscribirse(IObserverSensorTemperatura observer);
    void Desuscribirse(IObserverSensorTemperatura observer);
    void Notificar();
}