namespace AccesoAlimentario.Core.Entities.Incidentes;

public abstract class Incidente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public virtual List<VisitaTecnica> VisitasTecnicas { get; set; } = [];
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public bool Resuelto { get; set; } = false;

    public Incidente()
    {
    }
    
    public Incidente(DateTime fecha, bool resuelto)
    {
        Fecha = fecha;
        Resuelto = resuelto;
    }
    
    public void AgregarVisitaTecnica(VisitaTecnica visitaTecnica)
    {
        VisitasTecnicas.Add(visitaTecnica);
    }
}