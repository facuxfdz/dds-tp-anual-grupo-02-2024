using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public abstract class Incidente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public List<VisitaTecnica> VisitasTecnicas { get; set; } = [];
    public DateTime Fecha { get; set; } = DateTime.Now;
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