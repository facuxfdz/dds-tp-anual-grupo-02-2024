using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;


namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaConsumo : Tarjeta
{
    private Colaborador _responsable;
    public TarjetaConsumo(Colaborador responsable, string codigo, PersonaVulnerable personaVulnerable) : base(codigo, personaVulnerable)
    {
        _responsable = responsable;
    }
}