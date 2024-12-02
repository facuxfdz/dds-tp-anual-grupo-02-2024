using AccesoAlimentario.Core.Entities.DocumentosIdentidad;

namespace AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;

public class DocumentoIdentidadRequest : IDtoRequest
{
    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.DNI;
    public string NroDocumento { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; } = new DateTime();

    public bool Validar()
    {
        return !string.IsNullOrEmpty(NroDocumento)
               && FechaNacimiento.Year > 1900;
    }
}