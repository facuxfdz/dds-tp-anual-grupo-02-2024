using AccesoAlimentario.Core.Entities.Contribuciones;

namespace AccesoAlimentario.Core.Interfaces.Validadores;

public interface IValidadorContribuciones
{
    public void Validar(FormaContribucion formaContribucion);
}