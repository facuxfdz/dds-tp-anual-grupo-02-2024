using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public abstract class FormaContribucion
{
    protected readonly ValidadorContribuciones _validadorContribuciones;
    private DateTime _fechaContribucion;

    public FormaContribucion(DateTime fechaContribucion)
    {
        _fechaContribucion = fechaContribucion;
    }

    public abstract float CalcularPuntos();
    
    public bool EsValido(Colaborador colaborador)
    {
        return _validadorContribuciones.Validar(this, colaborador);
    }
}