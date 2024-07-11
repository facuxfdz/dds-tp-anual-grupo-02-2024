using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Roles;

public abstract class Rol
{
    public Persona _persona { get; }
    
    public Rol(Persona persona)
    {
        _persona = persona;
    }
}