using AccesoAlimentario.API.Domain.Personas;

namespace AccesoAlimentario.API.Controllers.RequestDTO;

public class PersonaDTO
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public DocumentoDTO? DocumentoIdentidad { get; set; }
    public DireccionDTO? Direccion { get; set; }
    public string? Sexo { get; set; }
    public string? TipoPersona { get; set; }
    public string? RazonSocial { get; set; }
    public TipoJuridico? TipoJuridico { get; set; }
    public string? Rubro { get; set; }
    
    
    
}