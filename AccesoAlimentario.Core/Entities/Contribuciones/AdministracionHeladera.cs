using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class AdministracionHeladera : FormaContribucion
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

    public override float CalcularPuntos()
    {
        return 0;
    }
}