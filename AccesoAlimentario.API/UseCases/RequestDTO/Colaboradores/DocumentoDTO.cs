using AccesoAlimentario.API.Domain.Personas;

namespace AccesoAlimentario.API.UseCases.RequestDTO.Colaboradores;

public class DocumentoDTO
{
    public TipoDocumento Tipo { get; set; }
    public string Numero { get; set; }
    public DateOnly FechaVencimiento { get; set; }
}