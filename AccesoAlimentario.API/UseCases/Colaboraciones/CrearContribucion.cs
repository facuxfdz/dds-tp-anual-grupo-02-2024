using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Domain.Premios;
using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.Colaboraciones.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.API.UseCases.Colaboraciones;

public class CrearContribucion(
    IRepository<Heladera> heladeraRepository,
    IRepository<Contribucion> contribucionRepository,
    IRepository<Colaborador> colaboradorRepository,
    IRepository<ViandaEstandar> viandaEstandarRepository
)
{
    private bool ValidarContribucion(ContribucionDTO contribucionDTO)
    {
        if (contribucionDTO.Tipo == TipoDistribucion.DonacionMonetaria)
        {
            return contribucionDTO.DonacionMonetariaMonto.HasValue &&
                   contribucionDTO.DonacionMonetariaFrecuenciaDias.HasValue;
        }
        else if (contribucionDTO.Tipo == TipoDistribucion.DonacionVianda)
        {
            return contribucionDTO.DonacionViandaHeladeraId.HasValue && contribucionDTO.DonacionViandaComida != null &&
                   contribucionDTO.DonacionViandaFechaDonacion.HasValue &&
                   contribucionDTO.DonacionViandaFechaCaducidad.HasValue &&
                   contribucionDTO.DonacionViandaColaboradorId.HasValue &&
                   contribucionDTO.DonacionViandaCalorias.HasValue && contribucionDTO.DonacionViandaPeso.HasValue &&
                   contribucionDTO.DonacionViandaEstadoVianda.HasValue &&
                   contribucionDTO.DonacionViandaUnidadEstandarId.HasValue;
        }
        else if (contribucionDTO.Tipo == TipoDistribucion.OfertaPremio)
        {
            return contribucionDTO.OfertaPremioNombre != null && contribucionDTO.OfertaPremioDescripcion != null &&
                   contribucionDTO.OfertaPremioPuntosNecesarios.HasValue &&
                   contribucionDTO.OfertaPremioImagen != null && contribucionDTO.OfertaPremioRubro.HasValue;
        }
        else if (contribucionDTO.Tipo == TipoDistribucion.AdministracionHeladera)
        {
            return contribucionDTO.AdministracionHeladeraNombre != null &&
                   contribucionDTO.AdministracionHeladeraLongitud.HasValue &&
                   contribucionDTO.AdministracionHeladeraLatitud.HasValue &&
                   contribucionDTO.AdministracionHeladeraCalle != null &&
                   contribucionDTO.AdministracionHeladeraNumero != null &&
                   contribucionDTO.AdministracionHeladeraLocalidad != null &&
                   contribucionDTO.AdministracionHeladeraPiso != null &&
                   contribucionDTO.AdministracionHeladeraDepartamento != null &&
                   contribucionDTO.AdministracionHeladeraCodigoPostal != null &&
                   contribucionDTO.AdministracionHeladeraFechaInstalacion.HasValue &&
                   contribucionDTO.AdministracionHeladeraTemperaturaMinimaConfig.HasValue;
        }
        else if (contribucionDTO.Tipo == TipoDistribucion.Viandas)
        {
            return contribucionDTO.DistribucionViandasHeladeraOrigenId.HasValue &&
                   contribucionDTO.DistribucionViandasHeladeraDestinoId.HasValue &&
                   contribucionDTO.DistribucionViandasCantViandas.HasValue &&
                   contribucionDTO.DistribucionViandasMotivoDistribucion.HasValue;
        }

        return false;
    }

    public void Crear(ContribucionDTO contribucionDTO)
    {
        if (!ValidarContribucion(contribucionDTO))
        {
            throw new ContribucionInvalida();
        }

        switch (contribucionDTO.Tipo)
        {
            case TipoDistribucion.Viandas:
                var heladeraOrigen =
                    heladeraRepository.GetById(contribucionDTO.DistribucionViandasHeladeraOrigenId.Value);
                var heladeraDestino =
                    heladeraRepository.GetById(contribucionDTO.DistribucionViandasHeladeraDestinoId.Value);

                if (heladeraOrigen == null || heladeraDestino == null)
                {
                    throw new HeladeraNoExiste();
                }

                var contribucionViandas = new DistribucionViandas(
                    contribucionDTO.Fecha,
                    heladeraOrigen,
                    heladeraDestino,
                    contribucionDTO.DistribucionViandasCantViandas.Value,
                    contribucionDTO.DistribucionViandasMotivoDistribucion.Value
                );

                contribucionRepository.Insert(contribucionViandas);
                break;
            case TipoDistribucion.DonacionVianda:
                var heladera = heladeraRepository.GetById(contribucionDTO.DonacionViandaHeladeraId.Value);
                var colaborador = colaboradorRepository.GetById(contribucionDTO.DonacionViandaColaboradorId.Value);
                var viandaEstandar = viandaEstandarRepository.GetById(contribucionDTO.DonacionViandaUnidadEstandarId.Value);
                
                var vianda = new Vianda(
                    contribucionDTO.DonacionViandaComida,
                    contribucionDTO.DonacionViandaFechaDonacion.Value,
                    contribucionDTO.DonacionViandaFechaCaducidad.Value,
                    colaborador,
                    heladera,
                    contribucionDTO.DonacionViandaCalorias.Value,
                    contribucionDTO.DonacionViandaPeso.Value,
                    contribucionDTO.DonacionViandaEstadoVianda.Value,
                    viandaEstandar
                );

                var donacionVianda = new DonacionVianda(
                    contribucionDTO.Fecha,
                    heladera,
                    vianda
                );
                
                contribucionRepository.Insert(donacionVianda);
                break;
            case TipoDistribucion.OfertaPremio:
                var premio = new Premio(
                    contribucionDTO.OfertaPremioNombre,
                    contribucionDTO.OfertaPremioDescripcion,
                    contribucionDTO.OfertaPremioPuntosNecesarios.Value,
                    contribucionDTO.OfertaPremioImagen,
                    contribucionDTO.OfertaPremioRubro.Value
                );
                
                var ofertaPremio = new OfertaPremio(
                    contribucionDTO.Fecha,
                    premio
                );
                
                contribucionRepository.Insert(ofertaPremio);
                break;
            case TipoDistribucion.AdministracionHeladera:
                var direccion = new Direccion(
                    contribucionDTO.AdministracionHeladeraCalle,
                    contribucionDTO.AdministracionHeladeraNumero,
                    contribucionDTO.AdministracionHeladeraLocalidad,
                    contribucionDTO.AdministracionHeladeraPiso,
                    contribucionDTO.AdministracionHeladeraDepartamento,
                    contribucionDTO.AdministracionHeladeraCodigoPostal
                );
                
                var puntoEstrategico = new PuntoEstrategico(
                    contribucionDTO.AdministracionHeladeraNombre,
                    contribucionDTO.AdministracionHeladeraLongitud.Value,
                    contribucionDTO.AdministracionHeladeraLatitud.Value,
                    direccion
                );
                
                var h = new Heladera(
                    puntoEstrategico,
                    contribucionDTO.AdministracionHeladeraTemperaturaMinimaConfig.Value,
                    contribucionDTO.AdministracionHeladeraTemperaturaMaximaConfig.Value,
                    new ModeloHeladera()
                );
                
                var administracionHeladera = new AdministracionHeladera(
                    contribucionDTO.Fecha,
                    h
                );
                
                contribucionRepository.Insert(administracionHeladera);
                break;
            case TipoDistribucion.DonacionMonetaria:
                var donacionMonetaria = new DonacionMonetaria(
                    contribucionDTO.Fecha,
                    contribucionDTO.DonacionMonetariaMonto.Value,
                    contribucionDTO.DonacionMonetariaFrecuenciaDias.Value
                );
                
                contribucionRepository.Insert(donacionMonetaria);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}