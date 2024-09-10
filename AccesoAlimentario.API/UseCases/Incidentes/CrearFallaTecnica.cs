using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Incidentes;
using AccesoAlimentario.API.Infrastructure.Controllers;
using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.Colaboradores.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Incentes;

namespace AccesoAlimentario.API.UseCases.Incidentes;

public class CrearFallaTecnica(
    IRepository<Heladera> heladeraRepository,
    IRepository<Incidente> incidenteRepository,
    IRepository<Colaborador> colaboradorRepository
)
{
    private bool dtoValido(FallaTecnicaDTO fallaTecnicaReq)
    {
        return true;
    }
    
    public void Crear(FallaTecnicaDTO fallaTecnicaReq)
    {
        if(!dtoValido(fallaTecnicaReq))
        {
            throw new RequestInvalido("Request invalido para crear incidente");
        }
        
        var heladera = heladeraRepository.Get(
            filter: h => h.Id == fallaTecnicaReq.HeladeraId
        ).FirstOrDefault();
        
        if(heladera == null)
        {
            throw new HeladeraNoExiste();
        }
        
        var colaborador = colaboradorRepository.Get(
            filter: c => c.Id == fallaTecnicaReq.ColaboradorId
        ).FirstOrDefault();
        
        if(colaborador == null)
        {
            throw new ColaboradorNoExiste();
        }
        
        var fallaTecnica = new FallaTecnica(
            fecha: DateTime.Now,
            heladera: heladera,
            colaborador: colaborador,
            descripcion: fallaTecnicaReq.Descripcion,
            foto: fallaTecnicaReq.Foto
        );
        
        incidenteRepository.Insert(fallaTecnica);
    }
}