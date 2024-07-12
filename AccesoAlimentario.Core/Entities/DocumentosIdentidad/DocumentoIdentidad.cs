using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

public class DocumentoIdentidad
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public TipoDocumento TipoDocumento { get; private set; }
    public int NroDocumento { get; private set; }
    public DateOnly? FechaNacimiento { get; private set; }
    public DocumentoIdentidad(TipoDocumento tipoDocumento, int nroDocumento, DateOnly? fechaNacimiento)
    {
        TipoDocumento = tipoDocumento;
        NroDocumento = nroDocumento;
        FechaNacimiento = fechaNacimiento;
    }
}