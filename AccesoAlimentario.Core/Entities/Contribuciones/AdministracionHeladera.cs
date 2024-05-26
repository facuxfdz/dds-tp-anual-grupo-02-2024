using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class AdministracionHeladera : FormaContribucion
{
    private Heladera _heladera;

    public AdministracionHeladera(Colaborador colaborador, IValidadorContribuciones validadorContribuciones, DateTime fechaContribucion, Heladera heladera)
        : base(colaborador, validadorContribuciones, fechaContribucion)
    {
        _heladera = heladera;
    }

    public override void Colaborar()
    {
        throw new NotImplementedException();
    }

    public override float CalcularPuntos()
    {
        throw new NotImplementedException();
    }
}