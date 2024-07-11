namespace AccesoAlimentario.Core.Servicios;

{
    public class ColaboracionesServicio
    {
        public FormaContribucion crearAdministracionHeladera(Heladera heladera, DateTime fechaContr)
        {
            heladera
        }

        public FormaContribucion crearDistribucionViandas(HOrigen hOrigen, HDestino hDestino, int cant, string motivo, DateTime fechaContr)
        {
            // TODO: HACER
            throw new NotImplementedException();
        }

        public FormaContribucion crearRegistroPersonaVulnerable(Persona persona, int menores, Tarjeta tarjeta, DateTime fechaContr)
        {
            // TODO: HACER
            throw new NotImplementedException();
        }

        public FormaContribucion crearDonacionMonetaria(decimal monto, int frecDias, DateTime fecha, DateTime fechaContr)
        {
            // TODO: HACER
            throw new NotImplementedException();
        }

        public FormaContribucion crearDonacionVianda(Heladera heladera, Vianda vianda, DateTime fechaContr)
        {
            // TODO: HACER
            throw new NotImplementedException();
        }

        public FormaContribucion crearOfertaPremio(Premio premio, DateTime fechaContr)
        {
            // TODO: HACER
            throw new NotImplementedException();
        }
    }
}