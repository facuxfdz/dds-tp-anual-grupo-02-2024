namespace AccesoAlimentario.Core.Entities.Heladeras;

public interface IObserverSensorTemperatura
{
    void CambioSensorTemperatura(float dato, bool error);
}