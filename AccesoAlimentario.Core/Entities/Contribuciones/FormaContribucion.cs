using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public abstract class FormaContribucion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime FechaContribucion { get; set; } = DateTime.Now;

    public FormaContribucion()
    {
    }

    public FormaContribucion(DateTime fechaContribucion)
    {
        FechaContribucion = fechaContribucion;
    }
}