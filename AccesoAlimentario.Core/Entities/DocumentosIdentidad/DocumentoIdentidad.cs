using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.DocumentosIdentidad;

public class DocumentoIdentidad
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.DNI;
    public int NroDocumento { get; set; } = 0;
    public DateOnly? FechaNacimiento { get; set; } = null;
    
    public DocumentoIdentidad()
    {
    }
    
    public DocumentoIdentidad(TipoDocumento tipoDocumento, int nroDocumento, DateOnly fechaNacimiento)
    {
        TipoDocumento = tipoDocumento;
        NroDocumento = nroDocumento;
        FechaNacimiento = fechaNacimiento;
    }
}