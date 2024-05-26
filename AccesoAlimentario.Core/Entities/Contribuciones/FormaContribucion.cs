using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public abstract class FormaContribucion
{
    private Colaborador _colaborador;
    private IValidadorContribuciones _validadorContribuciones;
    private DateTime _fechaContribucion;

    public FormaContribucion(Colaborador colaborador, IValidadorContribuciones validadorContribuciones, DateTime fechaContribucion)
    {
        _colaborador = colaborador;
        _validadorContribuciones = validadorContribuciones;
        _fechaContribucion = fechaContribucion;
    }

    public abstract void Colaborar();
    public abstract float CalcularPuntos();


}