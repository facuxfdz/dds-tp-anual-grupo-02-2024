using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class OfertaPremio : FormaContribucion
{
    protected readonly ValidadorContribuciones _validadorContribuciones = new ValidadorOfertaPremio();
    private Premio _premio;

    public OfertaPremio(DateTime fechaContribucion, Premio premio)
        : base(fechaContribucion)
    {
        _premio = premio;
    }

}