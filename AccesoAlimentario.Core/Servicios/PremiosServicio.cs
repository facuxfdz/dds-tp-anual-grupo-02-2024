namespace AccesoAlimentario.Core.Servicios;
namespace AccesoAlimentario.Core.Servicios
{
    public class PremiosServicio
    {
        public void canjearPremio(Premio premio, Colaborador colaborador)
        {
            if (premio.GetPuntosNecesarios() > colaborador.GetPuntos())
            {
                throw new PuntosInsuficientesException();
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