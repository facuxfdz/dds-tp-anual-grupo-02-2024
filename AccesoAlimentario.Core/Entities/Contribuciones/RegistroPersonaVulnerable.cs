using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class RegistroPersonaVulnerable : FormaContribucion
{
    private List<Tarjeta> _tarjetas;


    public RegistroPersonaVulnerable(Colaborador colaborador, IValidadorContribuciones validadorContribuciones,
        DateTime fechaContribucion, List<Tarjeta> tarjetas)
        : base(colaborador, validadorContribuciones, fechaContribucion)
    {
        _tarjetas = tarjetas;
    }

    public override void Colaborar()
    {
        throw new NotImplementedException();
    }

    public override float CalcularPuntos()
    {
        return Config.TarjetasRepartidasCoef * _tarjetas.Count;
    }
}