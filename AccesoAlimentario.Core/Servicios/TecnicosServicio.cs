using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Servicios;

public class TecnicosServicio(UnitOfWork unitOfWork)
{
    public void Crear(Persona persona, float latitud, float longitud, float radio)
    {
        var areaCobertura = new AreaCobertura(latitud, longitud, radio);
        var tecnico = new Tecnico(persona, areaCobertura);
        unitOfWork.TecnicoRepository.Insert(tecnico);
    }

    public void Eliminar(int id)
    {
        var tecnico = unitOfWork.TecnicoRepository.GetById(id);
        unitOfWork.TecnicoRepository.Delete(tecnico);
    }

    public void Modificar(Tecnico tecnico, float latitud, float longitud, float radio)
    {
        
    }

    public ICollection<Tecnico> ObtenerTecnicosEnZona(float latitud, float longitud)
    {
        var tecnicos = unitOfWork.TecnicoRepository.Get();
        var tecnicosEnZona = tecnicos.Where(t => t.AreaCobertura.EsCercano(latitud, longitud)).ToList();
        return tecnicosEnZona;
    }
}