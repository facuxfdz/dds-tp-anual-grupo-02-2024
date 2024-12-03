using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class RegistroPersonaVulnerable : FormaContribucion
{
    public virtual TarjetaConsumo? Tarjeta { get; set; } = null!;
    
    public RegistroPersonaVulnerable()
    {
    }

    public RegistroPersonaVulnerable(DateTime fechaContribucion, TarjetaConsumo tarjeta)
        : base(fechaContribucion)
    {
        Tarjeta = tarjeta;
    }
}