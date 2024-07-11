using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public interface IObserverHeladera : IObserver<Heladera>
{
    public void CambioHeladera(Heladera heladera);
}