namespace AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

public class DocumentoIdentidad
{
    private TipoDocumento _tipoDocumento;
    private int _nroDocumento;
    private DateOnly? _fechaNacimiento;
    public DocumentoIdentidad(TipoDocumento tipoDocumento, int nroDocumento, DateOnly? fechaNacimiento)
    {
        _tipoDocumento = tipoDocumento;
        _nroDocumento = nroDocumento;
        _fechaNacimiento = fechaNacimiento;
    }

    public TipoDocumento TipoDocumento => _tipoDocumento;
    public int NroDocumento => _nroDocumento;
    public DateOnly? FechaNacimiento => _fechaNacimiento;
}