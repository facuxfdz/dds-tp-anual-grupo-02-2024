using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Validadores.Contribuciones;

public class ValidadorOfertaPremio : ValidadorContribuciones
{
    private new readonly List<TipoColaborador> _colaboradoresValidos =
    [
        TipoColaborador.PersonaJuridica
    ];

    public override bool Validar(FormaContribucion formaContribucion, Colaborador colaborador)
    {
        return base.Validar(formaContribucion, colaborador);
    }
}