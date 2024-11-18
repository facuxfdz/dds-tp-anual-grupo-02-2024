using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class AdministracionHeladera : FormaContribucion
{
    public virtual Heladera Heladera { get; set; } = null!;

    public AdministracionHeladera()
    {
    }

    public AdministracionHeladera(DateTime fechaContribucion, Heladera heladera)
        : base(fechaContribucion)
    {
        Heladera = heladera;
    }
}