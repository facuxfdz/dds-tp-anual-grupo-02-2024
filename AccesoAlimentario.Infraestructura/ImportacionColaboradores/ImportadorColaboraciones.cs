namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ImportadorColaboraciones
{
    private FormaImportacion _formaImportacion;
    
    public ImportadorColaboraciones(FormaImportacion formaImportacion)
    {
        _formaImportacion = formaImportacion;
    }

    public void Importar(string file)
    {
        var colaboradores = _formaImportacion.ImportarColaboradores(file);
        // TODO Buscar colaboradores existentes
        // TODO Guardar colaboradores nuevos y colaboraciones realizadas
    }
}