namespace AccesoAlimentario.Core.Entities.DocumentosIdentidad;

public class DocumentoIdentidad
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.DNI;
    public int NroDocumento { get; set; } = 0;
    public DateTime? FechaNacimiento { get; set; } = null;
    
    public DocumentoIdentidad()
    {
    }
    
    public DocumentoIdentidad(TipoDocumento tipoDocumento, int nroDocumento, DateTime fechaNacimiento)
    {
        TipoDocumento = tipoDocumento;
        NroDocumento = nroDocumento;
        FechaNacimiento = fechaNacimiento;
    }
}