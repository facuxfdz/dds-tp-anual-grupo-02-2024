using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionVianda : FormaContribucion
{
    public virtual Heladera Heladera { get; set; } = null!;
    public virtual Vianda Vianda { get; set; } = null!;

    public DonacionVianda()
    {
    }

    public DonacionVianda(DateTime fechaContribucion, Heladera heladera, Vianda vianda)
        : base(fechaContribucion)
    {
        Heladera = heladera;
        Vianda = vianda;
    }
}