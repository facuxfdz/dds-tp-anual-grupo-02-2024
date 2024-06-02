using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;
using AccesoAlimentario.Core.Resources;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class RegistroPersonaVulnerable : FormaContribucion
{
    private Tarjeta _tarjeta;


    public RegistroPersonaVulnerable(Colaborador colaborador, IValidadorContribuciones validadorContribuciones,
        DateTime fechaContribucion, Tarjeta tarjeta)
        : base(colaborador, validadorContribuciones, fechaContribucion)
    {
        _tarjeta = tarjeta;
    }

    public override float CalcularPuntos()
    {
        return Config.TarjetasRepartidasCoef;
    }
}