using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Servicios;

public class ModeloHeladeraServicio(UnitOfWork unitOfWork)
{
    public ModeloHeladera Crear(string id, float capacidad, float temperaturaMinima, float temperaturaMaxima)
    {
        var modelo = unitOfWork.ModeloHeladeraRepository.Get(m => m.Id == id).FirstOrDefault();
        if (modelo != null)
        {
            throw new Exception("El modelo de heladera ya existe");
        }
        modelo = new ModeloHeladera(id,capacidad, temperaturaMinima, temperaturaMaxima);
        return unitOfWork.ModeloHeladeraRepository.Insert(modelo);
    }
    
    public List<ModeloHeladera> Listar()
    {
        return unitOfWork.ModeloHeladeraRepository.Get().ToList();
    }
}