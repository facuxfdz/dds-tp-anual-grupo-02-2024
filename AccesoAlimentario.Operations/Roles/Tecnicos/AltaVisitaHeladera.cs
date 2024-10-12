using MediatR;
using Microsoft.AspNetCore.Http;

namespace AccesoAlimentario.Operations.Roles.Tecnicos;

public static class AltaVisitaHeladera
{
    public class AltaVisitaHeladeraCommand : IRequest<IResult>
    {
        public Guid IncidenteId { get; set; } = Guid.Empty;
        public string Foto { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Comentarios { get; set; } = string.Empty;
    }
}