using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;


namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaConsumo : Tarjeta
{
    private Colaborador _responsable;

    private PersonaVulnerable? _personaVulnerable;

    public TarjetaConsumo(Colaborador responsable, string codigo, PersonaVulnerable? personaVulnerable) : base(codigo,
        personaVulnerable)
    {
        _responsable = responsable;
    }

    public void setPersonaVulnerable(PersonaVulnerable personaVulnerable)
    {
        _personaVulnerable = personaVulnerable;
    }
}