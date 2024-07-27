using AccesoAlimentario.Core.Entities.Roles;


namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaConsumo : Tarjeta
{
    public Colaborador Responsable { get; private set; } = null!;

    public TarjetaConsumo()
    {
    }

    private PersonaVulnerable? _personaVulnerable;

    public TarjetaConsumo(Colaborador responsable, string codigo, PersonaVulnerable? personaVulnerable) : base(codigo,
        personaVulnerable)
    {
        Responsable = responsable;
    }

    public void SetPersonaVulnerable(PersonaVulnerable personaVulnerable)
    {
        _personaVulnerable = personaVulnerable;
    }
}