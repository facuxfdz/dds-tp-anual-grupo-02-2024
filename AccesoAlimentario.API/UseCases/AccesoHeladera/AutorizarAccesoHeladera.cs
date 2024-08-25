using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.Controllers.RequestDTO.AccesoHeladera;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;

namespace AccesoAlimentario.API.UseCases.AccesoHeladera;

public class AutorizarAccesoHeladera(
    IRepository<Colaborador> colaboradorRepository,
    IRepository<Heladera> heladeraRepository
    )
{
    public void CrearAutorizacion(AutorizacionDTO autorizacion)
    {
        var colaborador = colaboradorRepository.Get(
            filter: c => c.Id == autorizacion.Colaborador.Id,
            includeProperties: "TarjetaColaboracion"
        );
        IEnumerable<Colaborador> colaboradores = colaborador as Colaborador[] ?? colaborador.ToArray();
        if (!colaboradores.Any())
        {
            throw new ColaboradorNoExiste();
        }
        // Chequear si el colaborador tiene una tarjeta de colaboración
        if (colaboradores.First().TarjetaColaboracion == null)
        {
            throw new TarjetaInexistente();
        }
        var heladera = heladeraRepository.Get(
            filter: h => h.Id == autorizacion.Heladera.Id
        );
        IEnumerable<Heladera> heladeras = heladera as Heladera[] ?? heladera.ToArray();
        if (!heladeras.Any())
        {
            throw new HeladeraNoExiste();
        }
        // Crear autorización
        // La autorizacion expira en 3 horas
        var autorizacionHeladera = new AutorizacionHeladera(
            DateTime.Now.AddHours(3),
            heladeras.First(),
            colaboradores.First().TarjetaColaboracion
        );
    }
}