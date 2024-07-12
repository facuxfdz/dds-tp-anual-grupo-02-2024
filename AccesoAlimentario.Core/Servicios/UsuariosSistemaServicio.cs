using System.Linq.Expressions;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Servicios;

public class UsuariosSistemaServicio(UnitOfWork unitOfWork)
{
    public void Crear(Persona persona, string username, string password)
    {
        var usuario = new UsuarioSistema(persona, username, password);
        unitOfWork.UsuarioSistemaRepository.Insert(usuario);
    }

    public void Eliminar(int id)
    {
        var usuario = unitOfWork.UsuarioSistemaRepository.GetById(id);

        if (usuario == null)
        {
            throw new InvalidOperationException("No se encontró el usuario");
        }

        unitOfWork.UsuarioSistemaRepository.Delete(usuario);
    }

    public void Modificar(UsuarioSistema usuario, string username, string password)
    {
    }

    public bool Login(string username, string password)
    {
        Expression<Func<UsuarioSistema, bool>> filter = u => u.UserName == username && u.Password == password;
        return unitOfWork.UsuarioSistemaRepository.Get(filter: filter).Any();
    }
}