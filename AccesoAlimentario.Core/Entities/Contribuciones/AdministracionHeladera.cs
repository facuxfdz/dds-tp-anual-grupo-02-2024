using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class AdministracionHeladera : FormaContribucion
{
    private Heladera _heladera;

    public AdministracionHeladera(Colaborador colaborador, IValidadorContribuciones validadorContribuciones, DateTime fechaContribucion, Heladera heladera)
        : base(colaborador, fechaContribucion)
    {
        _heladera = heladera;
    }

    public override float CalcularPuntos()
    {
        throw new NotImplementedException();
    }
}