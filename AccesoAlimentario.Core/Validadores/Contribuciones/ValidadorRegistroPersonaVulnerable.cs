using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Validadores.Contribuciones;

public class ValidadorRegistroPersonaVulnerable : ValidadorContribuciones
{
    private new readonly List<TipoColaborador> _colaboradoresValidos =
    [
        TipoColaborador.PersonaHumana
    ];

    public override bool Validar(FormaContribucion formaContribucion, Colaborador colaborador)
    {
        return base.Validar(formaContribucion, colaborador);
    }
}