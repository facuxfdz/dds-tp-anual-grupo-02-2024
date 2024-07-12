using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionVianda : FormaContribucion
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

    public override float CalcularPuntos()
    {
        return AppSettings.Instance.ViandasDonadasCoef;
    }
}