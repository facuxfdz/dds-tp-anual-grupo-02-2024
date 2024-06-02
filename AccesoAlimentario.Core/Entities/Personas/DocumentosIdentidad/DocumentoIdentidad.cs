namespace AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

public class DocumentoIdentidad
{
    private TipoDocumento _tipoDocumento;
    private int _nroDocumento;
    private SexoDocumento _sexoDocumento;
    
    public DocumentoIdentidad(TipoDocumento tipoDocumento, int nroDocumento, SexoDocumento sexoDocumento)
    {
        _tipoDocumento = tipoDocumento;
        _nroDocumento = nroDocumento;
        _sexoDocumento = sexoDocumento;
    }
    
    public TipoDocumento TipoDocumento => _tipoDocumento;
    public int NroDocumento => _nroDocumento;
    public SexoDocumento SexoDocumento => _sexoDocumento;
    
    public void Actualizar(TipoDocumento tipoDocumento, int nroDocumento, SexoDocumento sexoDocumento)
    {
        _tipoDocumento = tipoDocumento;
        _nroDocumento = nroDocumento;
        _sexoDocumento = sexoDocumento;
    }
}