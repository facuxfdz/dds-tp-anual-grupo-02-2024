using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Incidentes;

public abstract class Incidente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public List<VisitaTecnica> VisitasTecnicas { get; set; } = [];
    public DateTime Fecha { get; set; } = DateTime.Now;
    public bool Resuelto { get; set; } = false;
    public Heladera Heladera { get; set; }

    public Incidente()
    {
    }
    
    public Incidente(DateTime fecha, bool resuelto, Heladera heladera)
    {
        Fecha = fecha;
        Resuelto = resuelto;
        Heladera = heladera;
    }
    
    public void AgregarVisitaTecnica(VisitaTecnica visitaTecnica)
    {
        VisitasTecnicas.Add(visitaTecnica);
    }
}