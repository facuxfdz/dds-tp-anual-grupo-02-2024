using System.Runtime.InteropServices.JavaScript;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public abstract class Incidente
{
    protected List<VisitaTecnica> _visitasTecnicas;
    protected DateTime _fecha;
    protected bool _resuelto;
    
    public Incidente()
    {
        _visitasTecnicas = new List<VisitaTecnica>();
        _fecha = DateTime.Now;
        _resuelto = false;
    }
}