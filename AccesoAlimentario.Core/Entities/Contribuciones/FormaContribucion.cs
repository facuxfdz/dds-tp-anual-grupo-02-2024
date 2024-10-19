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