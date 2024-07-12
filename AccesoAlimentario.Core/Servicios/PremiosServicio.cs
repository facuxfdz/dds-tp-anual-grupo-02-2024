using System.Linq.Expressions;
using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Servicios;

public class PremiosServicio(UnitOfWork unitOfWork)
{
    public void CanjearPremio(Premio premio, Colaborador colaborador)
    {
        if (premio.GetPuntosNecesarios() > colaborador.Puntos)
        {
            throw new InvalidOperationException("No tiene suficientes puntos para canjear el premio");
        }

        colaborador.DescontarPuntos(premio.GetPuntosNecesarios());
        premio.Reclamar(colaborador);
    }

    public ICollection<Premio> ObtenerPremios()
    {
        Expression<Func<Premio, bool>> filter = c => c.ReclamadoPor == null;
        return unitOfWork.PremioRepository.Get(filter: filter).ToList();
    }
}