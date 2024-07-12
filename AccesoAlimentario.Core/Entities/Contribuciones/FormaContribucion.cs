using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public abstract class FormaContribucion
{
    private DateTime _fechaContribucion;

    public FormaContribucion()
    {
    }
    public FormaContribucion(DateTime fechaContribucion)
    {
        _fechaContribucion = fechaContribucion;
    }
    
}