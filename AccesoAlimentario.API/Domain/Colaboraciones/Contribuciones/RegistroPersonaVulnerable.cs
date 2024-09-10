namespace AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;

public class RegistroPersonaVulnerable : Contribucion
{
    public Tarjeta Tarjeta { get; set; } = null!;
    
    public RegistroPersonaVulnerable()
    {
    }

    public RegistroPersonaVulnerable(DateTime fechaContribucion, Tarjeta tarjeta)
        : base(fechaContribucion)
    {
        Tarjeta = tarjeta;
    }
}