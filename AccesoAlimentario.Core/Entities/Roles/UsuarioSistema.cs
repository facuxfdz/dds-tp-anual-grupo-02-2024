using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Usuarios;

public class UsuarioSistema : Rol
{
    private string _userName;
    private string _password;

    public UsuarioSistema(int id, Persona persona, string userName, string password)
        : base(id, persona)
    {
        _userName = userName;
        _password = password;
    }

    public void Actualizar(string userName, string password)
    {
        _userName = userName;
        _password = password;
    }
}