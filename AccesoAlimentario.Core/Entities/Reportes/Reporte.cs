using AccesoAlimentario.Core.Entities.Reportes;

namespace AccesoAlimentario.Core.Entities.Reportes;
public class Reporte{
    private string _descripcion;
    private DateTime _fechaCreacion;
    private List<EntradaReporte> _entradas;

    public Reporte(string descripcion, List<EntradaReporte> entradas){
        _descripcion = descripcion;
        _fechaCreacion = DateTime.Now;
        _entradas = entradas;
    }
}