using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AccesoHeladera
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public virtual Tarjeta Tarjeta { get; set; } = null!;
    public virtual List<Vianda> Viandas { get; set; } = [];
    public DateTime FechaAcceso { get; set; } = DateTime.UtcNow;
    public TipoAcceso TipoAcceso { get; set; } = TipoAcceso.IngresoVianda;
    public virtual Heladera Heladera { get; set; } = null!;
    public virtual AutorizacionManipulacionHeladera? Autorizacion { get; set; } = null!;
    
    public AccesoHeladera() { }
    
    public AccesoHeladera(Tarjeta tarjeta, Heladera heladera, TipoAcceso tipoAcceso)
    {
        Tarjeta = tarjeta;
        Heladera = heladera;
        TipoAcceso = tipoAcceso;
    }

    public bool VerificarValidez()
    {
        if (Tarjeta is not TarjetaColaboracion tarjeta) return true;
        return tarjeta.TieneAutorizacion(Heladera) != null;
    }
}