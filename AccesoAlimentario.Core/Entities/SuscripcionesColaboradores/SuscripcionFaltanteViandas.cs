using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;

public class SuscripcionFaltanteViandas : Suscripcion
{
    public int Minimo { get; set; } = 0;

    public SuscripcionFaltanteViandas()
    {
    }

    public SuscripcionFaltanteViandas(int minimo, Heladera heladera, Colaborador colaborador) : base(heladera,
        colaborador)
    {
        Minimo = minimo;
    }

    public override void CambioHeladera(Heladera heladera, CambioHeladeraTipo cambio)
    {
        if (cambio != CambioHeladeraTipo.CambioViandas) return;
        if (heladera.ObtenerCantidadDeViandas() < Minimo)
        {
            NotificarColaborador(
                new NotificacionFaltanteBuilder(heladera.ObtenerCantidadDeViandas())
                    .CrearNotificacion());
        }
    }
}