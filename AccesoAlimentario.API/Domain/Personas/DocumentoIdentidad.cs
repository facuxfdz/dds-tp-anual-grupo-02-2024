using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Personas;

public class DocumentoIdentidad
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.DNI;
    public string NroDocumento { get; set; } = "";
    public DateOnly? FechaNacimiento { get; set; } = null;
    
    public DocumentoIdentidad()
    {
    }
    
    public DocumentoIdentidad(TipoDocumento tipoDocumento, string nroDocumento, DateOnly fechaNacimiento)
    {
        TipoDocumento = tipoDocumento;
        NroDocumento = nroDocumento;
        FechaNacimiento = fechaNacimiento;
    }
}