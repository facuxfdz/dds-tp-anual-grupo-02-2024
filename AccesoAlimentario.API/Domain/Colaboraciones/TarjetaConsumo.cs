using AccesoAlimentario.API.Domain.Personas;

namespace AccesoAlimentario.API.Domain.Colaboraciones;

public class TarjetaConsumo : Tarjeta
{
    public Colaborador Responsable { get; private set; } = null!;
    
    public TarjetaConsumo()
    {
    }
    public TarjetaConsumo(Colaborador responsable, string codigo, Persona? propietario) : base(codigo,
        propietario)
    {
        Responsable = responsable;
    }
    
}