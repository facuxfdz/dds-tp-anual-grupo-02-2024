using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;
using AccesoAlimentario.Core.Resources;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionVianda : FormaContribucion
{
    private Heladera _heladera;
    private Vianda _vianda;

    public DonacionVianda(Colaborador colaborador, IValidadorContribuciones validadorContribuciones,
        DateTime fechaContribucion, Heladera heladera, Vianda vianda)
        : base(colaborador, fechaContribucion)
    {
        _heladera = heladera;
        _vianda = vianda;
    }

    public override float CalcularPuntos()
    {
        return Config.ViandasDonadasCoef;
    }
}