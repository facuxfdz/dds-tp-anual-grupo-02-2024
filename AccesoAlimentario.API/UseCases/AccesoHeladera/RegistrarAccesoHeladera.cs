
using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.AccesoHeladera;

namespace AccesoAlimentario.API.UseCases.AccesoHeladera;

public class RegistrarAccesoHeladera(
    IRepository<Tarjeta> tarjetaRepository,
    IRepository<Heladera> heladeraRepository,
    IRepository<Domain.Heladeras.AccesoHeladera> accesoHeladeraRepository,
    IRepository<AutorizacionHeladera> autorizacionHeladeraRepository
    )
{
    public void RegistrarAcceso(AccesoHeladeraDTO accesoHeladera)
    {
        var tarjeta = tarjetaRepository.Get(
            filter: t => t.Id == accesoHeladera.TarjetaId
        );
        IEnumerable<Tarjeta> tarjetas = tarjeta as Tarjeta[] ?? tarjeta.ToArray();
        if (!tarjetas.Any())
        {
            throw new TarjetaInexistente();
        }
        var heladera = heladeraRepository.Get(
            filter: h => h.Id == accesoHeladera.HeladeraId
        );
        IEnumerable<Heladera> heladeras = heladera as Heladera[] ?? heladera.ToArray();
        if (!heladeras.Any())
        {
            throw new HeladeraNoExiste();
        }

        var autorizacion = autorizacionHeladeraRepository.Get(
            filter: a => a.Heladera.Id == accesoHeladera.HeladeraId
                         && a.TarjetaAutorizada.Id == accesoHeladera.TarjetaId
        );
        IEnumerable<Domain.Heladeras.AutorizacionHeladera> autorizaciones = autorizacion as Domain.Heladeras.AutorizacionHeladera[] ?? autorizacion.ToArray();
        var ultimaAutorizacion = autorizaciones.MaxBy(a => a.FechaExpiracion);
        if (ultimaAutorizacion == null || !autorizaciones.Any() || ultimaAutorizacion.FechaExpiracion < DateTime.Now)
        {
            throw new AccesoNoAutorizado();
        }
        
        var acceso = new Domain.Heladeras.AccesoHeladera(
            tarjetas.First(),
            heladeras.First(),
            accesoHeladera.TipoAcceso,
            autorizaciones.First()
        );
        accesoHeladeraRepository.Insert(acceso);
    }
}