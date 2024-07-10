using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Usuarios;

namespace AccesoAlimentario.Core.Entities.Usuarios;

public class UsuarioSistema : Rol
{
    private string _nombre;
    private string _contrasenia;
    private bool _administrador;

    public UsuarioSistema(Persona persona, string userName, string password, bool administrador) 
        : base(persona)
    {
        _nombre = userName;
        _contrasenia = password;
        _administrador = _administrador;
    }
}