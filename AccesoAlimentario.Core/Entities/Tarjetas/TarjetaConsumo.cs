using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;


namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaConsumo : Tarjeta
{
    private Colaborador _responsable;    
    public TarjetaConsumo(String codigo, Persona propietario, Colaborador responsable) // TODO restringir persona solo a tipo personaVulnerable
    : base(codigo, propietario)
    {
        _responsable = responsable;
        _accesos = new List<AccesoHeladera>();
    }


}