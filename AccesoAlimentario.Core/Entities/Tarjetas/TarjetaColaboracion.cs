using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaColaboracion : Tarjeta
{
    private List<AutorizacionManipulacionHeladera> _autorizaciones;

    public TarjetaColaboracion(string codigo, Colaborador propietario)
        : base(codigo, propietario)
    {
        _autorizaciones = new List<AutorizacionManipulacionHeladera>();
    }

    public AutorizacionManipulacionHeladera? TieneAutorizacion(Heladera heladera)
    {
        return _autorizaciones.Find(autorizacion => autorizacion.Heladera == heladera && autorizacion.FechaExpiracion > DateTime.Now);
    }

    public void AgregarAutorizacion(AutorizacionManipulacionHeladera autorizacion)
    {
        _autorizaciones.Add(autorizacion);
    }
}