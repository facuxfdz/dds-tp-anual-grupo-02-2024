using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public interface ISubjectHeladera
{
    void Suscribirse(IObserverHeladera obs);
    void Desuscribirse(IObserverHeladera obs);
    void Notificar();
}