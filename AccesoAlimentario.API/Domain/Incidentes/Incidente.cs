using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public class Incidente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    List<VisitaTecnica> VisitasTecnicas { get; set; } = [];
    public DateTime Fecha { get; set; } = DateTime.Now;
    public bool Resuelto { get; set; } = false;
    public Heladera Heladera { get; set; } = null!;

    public Incidente(DateTime fecha, Heladera heladera)
    {
        Fecha = fecha;
        Heladera = heladera;
    }
}