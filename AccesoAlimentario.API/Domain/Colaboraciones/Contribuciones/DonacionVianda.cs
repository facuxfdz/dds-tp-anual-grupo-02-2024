using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;

public class DonacionVianda : Contribucion
{
    public Heladera Heladera { get; set; } = null!;
    public Vianda Vianda { get; set; } = null!;

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