using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Servicios;

public class UsuarioServicio
{
    public void Crear(Persona persona, string username, string password)
    {
    }

    public void Eliminar(UsuarioSistema usuario)
    {
    }

    public void Modificar(UsuarioSistema usuario, string username, string password)
    {

    }

    public bool Login(string username, string password)
    {
        return false;
    }
}