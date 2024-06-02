using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Validadores.Contribuciones;

public class ValidadorDonacionMonetaria : ValidadorContribuciones
{
    private new readonly List<TipoColaborador> _colaboradoresValidos =
    [
        TipoColaborador.PersonaHumana,
        TipoColaborador.PersonaJuridica
    ];

    public override bool Validar(FormaContribucion formaContribucion, Colaborador colaborador)
    {
        return base.Validar(formaContribucion, colaborador);
    }
}