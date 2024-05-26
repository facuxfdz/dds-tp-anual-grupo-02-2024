using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionVianda : FormaContribucion
{
    private Heladera _heladera;
    private Vianda _vianda;

    public DonacionVianda(Colaborador colaborador, IValidadorContribuciones validadorContribuciones,
        DateTime fechaContribucion, Heladera heladera, Vianda vianda)
        : base(colaborador, validadorContribuciones, fechaContribucion)
    {
        _heladera = heladera;
        _vianda = vianda;
    }

    public override void Colaborar()
    {
        throw new NotImplementedException();
    }

    public override float CalcularPuntos()
    {
        return Config.ViandasDonadasCoef;
    }
}