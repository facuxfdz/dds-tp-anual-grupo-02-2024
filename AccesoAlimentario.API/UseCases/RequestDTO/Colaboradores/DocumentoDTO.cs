using AccesoAlimentario.API.Domain.Personas;

namespace AccesoAlimentario.API.Controllers.RequestDTO;

public class DocumentoDTO
{
    public TipoDocumento Tipo { get; set; }
    public string Numero { get; set; }
    public DateOnly FechaVencimiento { get; set; }
}