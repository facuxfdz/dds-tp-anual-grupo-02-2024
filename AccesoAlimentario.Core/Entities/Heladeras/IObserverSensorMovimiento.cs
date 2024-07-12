namespace AccesoAlimentario.Core.Entities.Heladeras;

public interface IObserverSensorMovimiento
{
    void CambioSensorMovimiento(bool dato, bool error);
}