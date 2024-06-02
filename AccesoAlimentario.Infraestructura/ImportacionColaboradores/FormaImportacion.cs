namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public abstract class FormaImportacion
{
    public abstract List<DatosColaboracion> leerArchivo(String file);
}