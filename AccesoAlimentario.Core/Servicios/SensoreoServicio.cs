using AccesoAlimentario.Core.DAL;

namespace AccesoAlimentario.Core.Servicios;

public class SensoreoServicio(UnitOfWork unitOfWork)
{
    public void IngresarDataMovimiento(int idSensor, string data)
    {
        var sensor = unitOfWork.SensorMovimientoRepository.GetById(idSensor);
        if (sensor == null)
        {
            throw new Exception("Sensor no encontrado");
        }

        sensor.Registrar(DateTime.Now, data);
        unitOfWork.SensorMovimientoRepository.Update(sensor);
    }

    public void IngresarDataTemperatura(int idSensor, string data)
    {
        var sensor = unitOfWork.SensorTemperaturaRepository.GetById(idSensor);
        if (sensor == null)
        {
            throw new Exception("Sensor no encontrado");
        }

        sensor.Registrar(DateTime.Now, data);
        unitOfWork.SensorTemperaturaRepository.Update(sensor);
    }
}