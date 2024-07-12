using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Servicios;

public class ColaboracionesServicio
{
    public FormaContribucion CrearAdministracionHeladera(Heladera heladera, DateTime fechaContr)
    {
        //TODO
        throw new NotImplementedException();
    }

    public FormaContribucion CrearDistribucionViandas(Heladera heladeraOrigen, Heladera heladeraDestino, int cant, string motivo, DateTime fechaContr)
    {
        // TODO: HACER
        throw new NotImplementedException();
    }

    public FormaContribucion CrearRegistroPersonaVulnerable(Persona persona, int menores, Tarjeta tarjeta, DateTime fechaContr)
    {
        // TODO: HACER
        throw new NotImplementedException();
    }

    public FormaContribucion CrearDonacionMonetaria(decimal monto, int frecDias, DateTime fecha, DateTime fechaContr)
    {
        // TODO: HACER
        throw new NotImplementedException();
    }

    public FormaContribucion CrearDonacionVianda(Heladera heladera, Vianda vianda, DateTime fechaContr)
    {
        // TODO: HACER
        throw new NotImplementedException();
    }

    public FormaContribucion CrearOfertaPremio(Premio premio, DateTime fechaContr)
    {
        // TODO: HACER
        throw new NotImplementedException();
    }
}