using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AccesoHeladera
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Tarjeta Tarjeta { get; set; } = null!;
    public List<Vianda> Viandas { get; set; } = [];
    public DateTime FechaAcceso { get; set; } = DateTime.Now;
    public TipoAcceso TipoAcceso { get; set; } = TipoAcceso.IngresoVianda;
    public Heladera Heladera { get; set; } = null!;
    public AutorizacionManipulacionHeladera? Autorizacion { get; set; } = null!;
    
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