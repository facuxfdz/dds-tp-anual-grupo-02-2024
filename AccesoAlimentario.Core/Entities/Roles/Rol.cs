using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Roles;

public abstract class Rol
{
    public Persona _persona { get; }
    public int Id { get; private set; }
    public Rol()
    {
    }
    public Rol(int id, Persona persona)
    {
        Id = id;
        _persona = persona;
    }
}