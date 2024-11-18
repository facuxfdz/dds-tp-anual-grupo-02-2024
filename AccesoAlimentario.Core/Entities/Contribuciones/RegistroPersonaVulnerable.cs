using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class RegistroPersonaVulnerable : FormaContribucion
{
    public virtual Tarjeta? Tarjeta { get; set; } = null!;
    
    public RegistroPersonaVulnerable()
    {
    }

    public RegistroPersonaVulnerable(DateTime fechaContribucion, Tarjeta tarjeta)
        : base(fechaContribucion)
    {
        Tarjeta = tarjeta;
    }
}