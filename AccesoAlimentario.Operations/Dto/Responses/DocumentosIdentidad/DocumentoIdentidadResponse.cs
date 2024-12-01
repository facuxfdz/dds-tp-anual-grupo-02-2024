using AccesoAlimentario.Core.Entities.DocumentosIdentidad;

namespace AccesoAlimentario.Operations.Dto.Responses.DocumentosIdentidad;

public class DocumentoIdentidadResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.DNI;
    public int NroDocumento { get; set; } = 0;
    public DateTime? FechaNacimiento { get; set; } = null;
}