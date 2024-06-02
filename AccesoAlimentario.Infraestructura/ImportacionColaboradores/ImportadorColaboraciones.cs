namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ImportadorColaboraciones
{
    private FormaImportacion _formaImportacion;

    public void importar(String file)
    {
        _formaImportacion.leerArchivo(file);
    }
}