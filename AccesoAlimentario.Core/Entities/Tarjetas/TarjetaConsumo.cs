using AccesoAlimentario.Core.Entities.Roles;


namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaConsumo : Tarjeta
{
    public Colaborador Responsable { get; private set; } = null!;

    public TarjetaConsumo()
    {
    }

    public TarjetaConsumo(Colaborador responsable, string codigo, PersonaVulnerable personaVulnerable) : base(codigo,
        personaVulnerable)
    {
        Responsable = responsable;
    }
}