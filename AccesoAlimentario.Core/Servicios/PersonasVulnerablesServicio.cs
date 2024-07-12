using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Servicios;

public class PersonasVulnerablesServicio(UnitOfWork unitOfWork)
{
    public void Eliminar(int id)
    {
        var persoVul = unitOfWork.PersonaVulnerableRepository.GetById(id);
        if (persoVul != null)
            unitOfWork.PersonaVulnerableRepository.Delete(persoVul);
        else
        {
            throw new Exception("No se encontro la Persona Vulnerable");
        }
    }

    public void Modificar(PersonaVulnerable personaVulnerable, int cantidadDeMenores)
    {
    }
}