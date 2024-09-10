using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Colaboraciones;

namespace AccesoAlimentario.API.Domain.Premios;

public class Premio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Nombre { get; set; } = null!;
    public float PuntosNecesarios { get; set; } = 0;
    public string Imagen { get; set; } = null!;
    public Colaborador? ReclamadoPor { get; set; } = null!;
    public DateTime FechaReclamo { get; set; } = DateTime.Now;
    public TipoRubro Rubro { get; set; } = TipoRubro.Otros;
}