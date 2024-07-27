using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public interface ISubjectHeladeraTemperatura
{
    void Suscribirse(IObserverSensorTemperatura observer);
    void Desuscribirse(IObserverSensorTemperatura observer);
    void Notificar(float dato, bool error);
}