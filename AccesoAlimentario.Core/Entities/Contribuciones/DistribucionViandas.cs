using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DistribucionViandas : FormaContribucion
{
    public Heladera HeladeraOrigen { get; set; } = null!;
    public Heladera HeladeraDestino { get; set; } = null!;
    public int CantViandas { get; set; } = 0;
    public MotivoDistribucion MotivoDistribucion { get; set; } = MotivoDistribucion.Desperfecto;

    public DistribucionViandas()
    {
    }
    
    public DistribucionViandas(DateTime fechaContribucion, Heladera heladeraOrigen,
        Heladera heladeraDestino, int cantViandas, MotivoDistribucion motivoDistribucion)
        : base(fechaContribucion)
    {
        HeladeraOrigen = heladeraOrigen;
        HeladeraDestino = heladeraDestino;
        CantViandas = cantViandas;
        MotivoDistribucion = motivoDistribucion;
    }
}