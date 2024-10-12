using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AutoMapper;

namespace AccesoAlimentario.Operations.Mappers;

public class DocumentoIdentidadMapper : Profile
{
    public DocumentoIdentidadMapper()
    {
        CreateMap<DocumentoIdentidadRequest, DocumentoIdentidad>();
    }
}