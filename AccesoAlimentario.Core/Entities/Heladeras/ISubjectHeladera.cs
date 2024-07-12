using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public interface ISubjectHeladera
{
    void Suscribirse(IObserverHeladera obs);
    void Desuscribirse(IObserverHeladera obs);
    void Notificar();
}