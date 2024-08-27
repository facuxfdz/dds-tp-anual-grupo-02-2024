using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Infrastructure.Controllers;
using AccesoAlimentario.API.UseCases.Heladeras.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera;
using AccesoAlimentario.API.UseCases.RequestDTO.Heladera.Excepciones;

namespace AccesoAlimentario.API.UseCases.Heladeras;

public class CrearHeladera(
    IRepository<Heladera> heladeraRepository,
    IRepository<PuntoEstrategico> puntoEstrategicoRepository,
    IRepository<ModeloHeladera> modeloHeladeraRepository
    )
{
    private bool dtoValido(HeladeraDTO heladeraReq)
    {
        return heladeraReq.PuntoEstrategico != null &&
               heladeraReq.Modelo != null;
    }
    public void Crear(HeladeraDTO heladeraReq)
    {
        if(!dtoValido(heladeraReq))
        {
            throw new RequestInvalido("Request invalido para crear heladera");
        }
        var puntoEstrategico = puntoEstrategicoRepository.Get(
            filter: p => heladeraReq.PuntoEstrategico != null && p.Id == heladeraReq.PuntoEstrategico.Id
        );
        IEnumerable<PuntoEstrategico> puntoEstrategicos = puntoEstrategico as PuntoEstrategico[] ?? puntoEstrategico.ToArray();
        if(!puntoEstrategicos.Any())
        {
            throw new PuntoNoExistente();
        }
        
        var modelo = modeloHeladeraRepository.Get(
            filter: m => heladeraReq.Modelo != null && m.Id == heladeraReq.Modelo.Id
        ).FirstOrDefault();
        if(modelo == null)
        {
            throw new ModeloHeladeraNoExiste();
        }
        var heladera = new Heladera(
            puntoEstrategico: puntoEstrategicos.First(),
            modelo: modelo,
            temperaturaMinima: modelo.TemperaturaMinima,
            temperaturaMaxima: modelo.TemperaturaMaxima
        );
        heladeraRepository.Insert(heladera);
    }
}