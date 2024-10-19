namespace AccesoAlimentario.Core.Entities.Sensores;

public abstract class Sensor
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public abstract void Registrar(DateTime fecha, string valor);
}