using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class AdministracionHeladera : FormaContribucion
{
    protected readonly ValidadorContribuciones _validadorContribuciones = new ValidadorAdministracionHeladera();
    private Heladera _heladera;

    public AdministracionHeladera(DateTime fechaContribucion, Heladera heladera)
        : base(fechaContribucion)
    {
        _heladera = heladera;
    }


}