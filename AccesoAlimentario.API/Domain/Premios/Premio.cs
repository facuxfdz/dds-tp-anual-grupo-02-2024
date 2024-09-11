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
    public string Descripcion { get; set; } = null!;
    public float PuntosNecesarios { get; set; } = 0;
    public string Imagen { get; set; } = null!;
    public Colaborador? ReclamadoPor { get; set; } = null!;
    public DateTime FechaReclamo { get; set; } = DateTime.Now;
    public TipoRubro Rubro { get; set; } = TipoRubro.Otros;
    
    public Premio()
    {
    }
    
    public Premio(string nombre, string descripcion, float puntosNecesarios, string imagen, TipoRubro rubro)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        PuntosNecesarios = puntosNecesarios;
        Imagen = imagen;
        Rubro = rubro;
    }
}