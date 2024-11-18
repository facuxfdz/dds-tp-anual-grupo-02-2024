using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Roles;

public abstract class Rol
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PersonaId { get; set; } = Guid.Empty;
    public virtual Persona Persona { get; set; } = null!;
    
    public Rol()
    {
    }
    
    public Rol(Persona persona)
    {
        Persona = persona;
    }
}