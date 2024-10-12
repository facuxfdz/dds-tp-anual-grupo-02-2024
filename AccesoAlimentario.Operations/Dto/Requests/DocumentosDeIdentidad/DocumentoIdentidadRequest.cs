using AccesoAlimentario.Core.Entities.DocumentosIdentidad;

namespace AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;

public class DocumentoIdentidadRequest : IDtoRequest
{
    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.DNI;
    public int NroDocumento { get; set; } = 0;
    public DateOnly FechaNacimiento { get; set; } = new DateOnly();

    public bool Validar()
    {
        return NroDocumento > 0
               && FechaNacimiento.Year > 1900;
    }
}