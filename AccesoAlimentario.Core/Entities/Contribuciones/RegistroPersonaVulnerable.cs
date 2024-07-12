using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class RegistroPersonaVulnerable : FormaContribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    private Tarjeta _tarjeta;

    public RegistroPersonaVulnerable()
    {
    }
    
    public RegistroPersonaVulnerable(DateTime fechaContribucion, Tarjeta tarjeta)
        : base(fechaContribucion)
    {
        _tarjeta = tarjeta;
    }

}