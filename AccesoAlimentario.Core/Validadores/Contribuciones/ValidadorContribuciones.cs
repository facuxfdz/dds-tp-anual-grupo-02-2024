using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Validadores.Contribuciones;

public abstract class ValidadorContribuciones
{
    protected List<TipoColaborador> _colaboradoresValidos;

    public virtual bool Validar(FormaContribucion formaContribucion, Colaborador colaborador)
    {
        throw new NotImplementedException();
        /*return _colaboradoresValidos.Contains(colaborador._persona.ObtenerTipoPersona());*/
    }
}