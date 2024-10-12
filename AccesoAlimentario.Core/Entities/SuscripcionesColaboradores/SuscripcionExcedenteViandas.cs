using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public class SuscripcionExcedenteViandas : Suscripcion
{
    public int Maximo { get; set; } = 0;

    public SuscripcionExcedenteViandas()
    {
    }

    public SuscripcionExcedenteViandas(int maximo, Heladera heladera, Colaborador colaborador) : base(heladera,
        colaborador)
    {
        Maximo = maximo;
    }

    public override void CambioHeladera(Heladera heladera, CambioHeladeraTipo cambio)
    {
        if (cambio != CambioHeladeraTipo.CambioViandas) return;
        if (heladera.ObtenerCantidadDeViandas() > Maximo)
        {
            NotificarColaborador(
                new NotificacionExcedenteBuilder(heladera.ObtenerCantidadDeViandas())
                    .CrearNotificacion());
        }
    }
}