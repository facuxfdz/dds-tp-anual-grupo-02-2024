using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Validadores.Contribuciones;

public abstract class ValidadorContribuciones
{
    protected List<TipoColaborador> _colaboradoresValidos;

    public virtual bool Validar(FormaContribucion formaContribucion, Colaborador colaborador)
    {
        return colaborador switch
        {
            PersonaJuridica => _colaboradoresValidos.Contains(TipoColaborador.PersonaJuridica),
            PersonaHumana => _colaboradoresValidos.Contains(TipoColaborador.PersonaHumana),
            _ => false
        };
    }
}