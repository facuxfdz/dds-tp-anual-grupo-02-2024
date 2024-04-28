using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Vaildadores;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public abstract class FormaContribucion
{
    private Colaborador _colaborador;
    private IValidador _validador;
    private DateTime _fechaContribucion;

    public FormaContribucion(Colaborador colaborador, IValidador validador, DateTime fechaContribucion)
    {
        _colaborador = colaborador;
        _validador = validador;
        _fechaContribucion = fechaContribucion;
    }

    public abstract void Colaborar();

}