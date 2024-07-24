namespace AccesoAlimentario.Core.Entities.Reportes;
public class Reporte{
    private string _descripcion;
    private DateTime _fechaCreacion;
    private string _cuerpo;

    public Reporte(string descripcion, string cuerpo){
        _descripcion = descripcion;
        _fechaCreacion = DateTime.Now;
        _cuerpo = cuerpo;
    }
}