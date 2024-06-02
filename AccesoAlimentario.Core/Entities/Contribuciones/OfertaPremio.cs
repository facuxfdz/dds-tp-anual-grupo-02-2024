using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class OfertaPremio : FormaContribucion
{
    private Premio _premio;

    public OfertaPremio(Colaborador colaborador, IValidadorContribuciones validadorContribuciones,
        DateTime fechaContribucion, Premio premio)
        : base(colaborador, validadorContribuciones, fechaContribucion)
    {
        _premio = premio;
    }

    public override float CalcularPuntos()
    {
        throw new NotImplementedException();
    }
}