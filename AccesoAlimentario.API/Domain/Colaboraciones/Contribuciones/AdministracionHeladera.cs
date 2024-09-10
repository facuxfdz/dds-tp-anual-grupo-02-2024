using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;

public class AdministracionHeladera : Contribucion
{
    public Heladera Heladera { get; set; } = null!;

    public AdministracionHeladera()
    {
    }

    public AdministracionHeladera(DateTime fechaContribucion, Heladera heladera)
        : base(fechaContribucion)
    {
        Heladera = heladera;
    }
}