using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Servicios
{
    public class PremiosServicio
    {
        public void canjearPremio(Premio premio, Colaborador colaborador)
        {
            if (premio.GetPuntosNecesarios() > colaborador.Puntos())
            {
                throw new PuntosInsuficientesException(); //TODO
            }
            colaborador.DescontarPuntos(premio.GetPuntosNecesarios()); //TODO, siento que esto deberia esatr dentro de Reclamar, pero no se si rompe encapsulamiento
            premio.Reclamar(colaborador);   
        }

        public Premio[] obtenerPremios()
        {
            // TODO: HACER
            return null; // Placeholder para que compile
        }
    }
}