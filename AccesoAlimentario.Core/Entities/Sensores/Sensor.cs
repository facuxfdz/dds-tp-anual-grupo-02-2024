using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Sensores;

public abstract class Sensor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid HeladeraId { get; set; } = Guid.Empty;
    public virtual Heladera Heladera { get; set; } = null!;

    public abstract Guid Registrar(DateTime fecha, string valor);
}