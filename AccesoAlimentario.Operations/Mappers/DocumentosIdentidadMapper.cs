using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Responses.DocumentosIdentidad;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class DocumentosIdentidadMapper : Profile
{
    public DocumentosIdentidadMapper()
    {
        CreateMap<DocumentoIdentidadRequest, DocumentoIdentidad>();
        CreateMap<DocumentoIdentidad, DocumentoIdentidadResponse>();
    }
}