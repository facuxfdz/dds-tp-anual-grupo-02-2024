using AccesoAlimentario.API.Controllers.RequestDTO;
using AccesoAlimentario.API.Controllers.RequestDTO.AccesoHeladera;
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;

namespace AccesoAlimentario.API.UseCases.AccesoHeladera;

public class AutorizarAccesoHeladera(
    IRepository<Colaborador> colaboradorRepository,
    IRepository<Heladera> heladeraRepository,
    IRepository<AutorizacionHeladera> autorizacionHeladeraRepository
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
        // Chequear si ya existe una autorizacion para esa heladera y tarjeta
        var autorizacionExistente = autorizacionHeladeraRepository.Get(
            filter: a => a.Heladera.Id == autorizacion.Heladera.Id
                         && a.TarjetaAutorizada.Id == colaboradores.First().TarjetaColaboracion.Id
        );
        IEnumerable<AutorizacionHeladera> autorizacionHeladeras = autorizacionExistente as AutorizacionHeladera[] ?? autorizacionExistente.ToArray();
        if (autorizacionHeladeras.Any() && autorizacionHeladeras.First().FechaExpiracion > DateTime.Now)
        {
            throw new AutorizacionEnVigencia(fechaExpiracion: autorizacionHeladeras.First().FechaExpiracion);
        }
        var autorizacionHeladera = new AutorizacionHeladera(
            DateTime.Now.AddHours(3),
            heladeras.First(),
            colaboradores.First().TarjetaColaboracion
        );
        // Crear autorización
        // La autorizacion expira en 3 horas
        autorizacionHeladeraRepository.Insert(autorizacionHeladera);
    }
}