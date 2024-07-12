using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Core.Servicios;

public class HeladerasServicio(UnitOfWork unitOfWork)
{
    public void Crear(int id, string nombre, float longi, float lat, Direccion direccion, ModeloHeladera modelo, float temperaturaMinima, float temperaturaMaxima) 
    {
        PuntoEstrategico puntoEstrategico = new PuntoEstrategico(id, nombre, longi, lat, direccion);
        Heladera heladera = new Heladera(puntoEstrategico, temperaturaMinima, temperaturaMaxima, modelo);
        unitOfWork.HeladerasRepository.Insert(heladera);
    }

    public void Eliminar(int id)
    {
        var heladera = unitOfWork.HeladerasRepository.GetById(id);
        if (heladera != null)
            unitOfWork.HeladerasRepository.Delete(heladera);
        else
        {
            throw new Exception("No se encontro la heladera");
        }
    }
    
    public void Modificar(Heladera heladera, float? temperaturaMinima, float? temperaturaMaxima)
    {
        //TODO persistencia de los cambios
        if (temperaturaMinima != null)
            heladera.TemperaturaMinimaConfig = temperaturaMinima.Value;

        if (temperaturaMaxima.HasValue)
            heladera.TemperaturaMaximaConfig = temperaturaMaxima.Value;
    }

    public Heladera Buscar(int id)
    {
        var heladera = unitOfWork.HeladerasRepository.GetById(id);
        if (heladera != null)
            return heladera;
        else
            throw new Exception("No se encontro la heladera");
    }

    public ICollection<Heladera> ObtenerTodos() 
    {
        return unitOfWork.Heladeras.Get();
    }

    public void AgregarSensor(Heladera heladera, ISensor sensor)
    {
        //TODO persistencia de los cambios
        heladera.AgregarSensor(sensor);
    }
    
    public void EliminarSensor(Heladera heladera, ISensor sensor)
    {
        //TODO persistencia de los cambios
        heladera.EliminarSensor(sensor);
    }
    
    public EstadoHeladera ObtenerEstadoHeladera(Heladera heladera)
    {
        return heladera.ObtenerEstadoHeladera();
    }
    
}