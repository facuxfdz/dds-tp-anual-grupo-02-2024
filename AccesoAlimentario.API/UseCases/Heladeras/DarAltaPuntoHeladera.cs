using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Infrastructure.Controllers;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera;

namespace AccesoAlimentario.API.UseCases.Heladeras;

public class DarAltaPuntoHeladera(IRepository<PuntoEstrategico> repository)
{
    private bool dtoValido(PuntoEstrategicoDTO puntoEstrategicoDTO)
    {
        Console.WriteLine("Campos: " + puntoEstrategicoDTO.Nombre + " " + puntoEstrategicoDTO.Latitud + " " + puntoEstrategicoDTO.Longitud + " " + puntoEstrategicoDTO.Direccion);
        return puntoEstrategicoDTO.Nombre != null && puntoEstrategicoDTO.Latitud != null && puntoEstrategicoDTO.Longitud != null && puntoEstrategicoDTO.Direccion != null;
    }
    public void AltaPunto(PuntoEstrategicoDTO puntoEstrategicoDTO)
    {
        if(!dtoValido(puntoEstrategicoDTO))
        {
            throw new RequestInvalido("El punto estratégico no es válido");
        }
        var direccion = new Direccion(
            puntoEstrategicoDTO.Direccion!.Calle,
            puntoEstrategicoDTO.Direccion.Numero,
            puntoEstrategicoDTO.Direccion.Localidad,
            puntoEstrategicoDTO.Direccion.CodigoPostal
        );
        // Buscar si ya existe un punto estrategico en esa direccion
        var puntoEstrategicoExistente = repository.Get(
            filter: p => p.Direccion.Calle == direccion.Calle &&
                         p.Direccion.Numero == direccion.Numero &&
                         p.Direccion.Localidad == direccion.Localidad &&
                         p.Direccion.CodigoPostal == direccion.CodigoPostal
        );
        if(puntoEstrategicoExistente.Any())
        {
            throw new RecursoYaExistente();
        }
        var puntoEstrategico = new PuntoEstrategico(
            puntoEstrategicoDTO.Nombre!,
            (float)puntoEstrategicoDTO.Longitud!,
            (float)puntoEstrategicoDTO.Latitud!,
            direccion
            );
        repository.Insert(puntoEstrategico);
    }
}