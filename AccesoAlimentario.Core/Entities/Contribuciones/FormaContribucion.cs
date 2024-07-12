using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public abstract class FormaContribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime FechaContribucion { get; set; } = DateTime.Now;

    public FormaContribucion()
    {
    }

    public FormaContribucion(DateTime fechaContribucion)
    {
        FechaContribucion = fechaContribucion;
    }

    public abstract float CalcularPuntos();
}