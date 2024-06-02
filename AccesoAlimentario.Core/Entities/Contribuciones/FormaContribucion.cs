using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public abstract class FormaContribucion
{
    private Colaborador _colaborador;
    private IValidadorContribuciones _validadorContribuciones;
    private DateTime _fechaContribucion;

    public FormaContribucion(Colaborador colaborador, DateTime fechaContribucion)
    {
        _colaborador = colaborador;
        _fechaContribucion = fechaContribucion;
    }

    public abstract float CalcularPuntos();

    public void AsignarColaborador(Colaborador colaborador)
    {
        _colaborador = colaborador;
    }


}