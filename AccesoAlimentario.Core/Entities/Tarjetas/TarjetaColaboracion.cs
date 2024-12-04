using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaColaboracion : Tarjeta
{
    public virtual List<AutorizacionManipulacionHeladera> Autorizaciones { get; set; } = [];

    public TarjetaColaboracion()
    {
    }

    public TarjetaColaboracion(string codigo, Colaborador propietario)
        : base(codigo, propietario)
    {
        Autorizaciones = [];
    }

    public AutorizacionManipulacionHeladera? TieneAutorizacion(Heladera heladera)
    {
        return Autorizaciones.Find(autorizacion =>
            autorizacion.Heladera == heladera && autorizacion.FechaExpiracion > DateTime.UtcNow);
    }

    public void AgregarAutorizacion(AutorizacionManipulacionHeladera autorizacion)
    {
        Autorizaciones.Add(autorizacion);
    }
}