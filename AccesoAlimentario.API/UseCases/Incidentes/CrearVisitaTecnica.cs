using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Incidentes;
using AccesoAlimentario.API.Domain.Tecnicos;
using AccesoAlimentario.API.Infrastructure.Controllers;
using AccesoAlimentario.API.UseCases.AccesoHeladera.Excepciones;
using AccesoAlimentario.API.UseCases.Incidentes.Excepciones;
using AccesoAlimentario.API.UseCases.RequestDTO.Incentes;

namespace AccesoAlimentario.API.UseCases.Incidentes;

public class CrearVisitaTecnica(
    IRepository<Incidente> incidenteRepository,
    IRepository<Tecnico> tecnicoRepository,
    IRepository<VisitaTecnica> visitaTecnicaRepository
)
{
    private bool dtoValido(VisitaTecnicaDTO visitaTecnicaReq)
    {
        return true;
    }

    public void Crear(VisitaTecnicaDTO visitaTecnicaReq)
    {
        if (!dtoValido(visitaTecnicaReq))
        {
            throw new RequestInvalido("Request invalido para crear incidente");
        }

        var incidente = incidenteRepository.Get(
            filter: c => c.Id == visitaTecnicaReq.IncidenteId
        ).FirstOrDefault();

        if (incidente == null)
        {
            throw new IncidenteNoExiste();
        }

        var tecnico = tecnicoRepository.Get(
            filter: c => c.Id == visitaTecnicaReq.TecnicoId
        ).FirstOrDefault();

        if (tecnico == null)
        {
            throw new TecnicoNoExiste();
        }

        var visitaTecnica = new VisitaTecnica(
            fecha: DateTime.Now,
            tecnico: tecnico,
            foto: visitaTecnicaReq.Foto,
            comentario: visitaTecnicaReq.Comentario
        );

        visitaTecnicaRepository.Insert(visitaTecnica);
    }
}