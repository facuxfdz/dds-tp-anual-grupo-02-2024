using AccesoAlimentario.Core.Entities.DocumentosIdentidad;

namespace AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;

public class DocumentoIdentidadRequest : IDtoRequest
{
    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.DNI;
    public int NroDocumento { get; set; } = 0;
    public DateTime FechaNacimiento { get; set; } = new DateTime();

    public bool Validar()
    {
        return NroDocumento > 0
               && FechaNacimiento.Year > 1900;
    }
}