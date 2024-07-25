namespace AccesoAlimentario.Core.Entities.Reportes;
public class Reporte{
    private string _descripcion;
    private DateTime _fechaCreacion;
    public string Cuerpo { get; private set; }

    public Reporte(string descripcion, string cuerpo){
        _descripcion = descripcion;
        _fechaCreacion = DateTime.Now;
        Cuerpo = cuerpo;
    }
}