using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public interface IObserverHeladera
{
    public void CambioHeladera(Heladera heladera, CambioHeladeraTipo cambio);
}