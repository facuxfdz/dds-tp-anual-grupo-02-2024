using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Core.Servicios;

public class HeladerasServicio(UnitOfWork unitOfWork)
{
    public void Crear(Direccion direccion, string modeloId,
        float temperaturaMinima, float temperaturaMaxima)
    {
        var puntoEstrategicoRepo = unitOfWork.PuntoEstrategicoRepository;
        var puntoEstrategico = puntoEstrategicoRepo.Get(
            p => 
                p.Direccion.Calle == direccion.Calle &&
                p.Direccion.Numero == direccion.Numero &&
                p.Direccion.Localidad == direccion.Localidad &&
                p.Direccion.CodigoPostal == direccion.CodigoPostal
                ).FirstOrDefault();
        if (puntoEstrategico == null)
        {
            Console.WriteLine("No se encontro el punto estrategico");
            throw new Exception("No se encontro el punto estrategico");
        }
        var modeloRepo = unitOfWork.ModeloHeladeraRepository;
        var modelo = modeloRepo.Get(m => m.Id == modeloId).FirstOrDefault();
        if (modelo == null)
        {
            Console.WriteLine("No se encontro el modelo de heladera");
            throw new Exception("No se encontro el modelo de heladera");
        }
        var heladera = new Heladera(puntoEstrategico, temperaturaMinima, temperaturaMaxima, modelo);
        unitOfWork.HeladeraRepository.Insert(heladera);
    }

    public void Eliminar(int id)
    {
        var heladera = unitOfWork.HeladeraRepository.GetById(id);
        if (heladera != null)
            unitOfWork.HeladeraRepository.Delete(heladera);
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

        unitOfWork.HeladeraRepository.Update(heladera);
    }

    public Heladera Buscar(int id)
    {
        var heladera = unitOfWork.HeladeraRepository.GetById(id);
        if (heladera != null)
            return heladera;

        throw new Exception("No se encontro la heladera");
    }

    public ICollection<Heladera> ObtenerTodos()
    {
        return unitOfWork.HeladeraRepository.Get().ToList();
    }

    public void AgregarSensor(Heladera heladera, Sensor sensor)
    {
        //TODO persistencia de los cambios
        heladera.AgregarSensor(sensor);
    }

    public void EliminarSensor(Heladera heladera, Sensor sensor)
    {
        //TODO persistencia de los cambios
        heladera.EliminarSensor(sensor);
    }

    public EstadoHeladera ObtenerEstadoHeladera(Heladera heladera)
    {
        return heladera.ObtenerEstadoHeladera();
    }
}