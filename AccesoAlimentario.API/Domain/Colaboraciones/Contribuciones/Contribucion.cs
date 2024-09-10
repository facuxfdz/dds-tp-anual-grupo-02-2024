using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;

public abstract class Contribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public DateTime FechaContribucion { get; private set; } = DateTime.Now;

    public Contribucion()
    {
    }

    public Contribucion(DateTime fechaContribucion)
    {
        FechaContribucion = fechaContribucion;
    }
}