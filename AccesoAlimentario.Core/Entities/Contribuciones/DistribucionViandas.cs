using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DistribucionViandas : FormaContribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    private Heladera _heladeraOrigen;
    private Heladera _heladeraDestino;
    public int CantViandas { get; private set; }
    private MotivoDistribucion _motivoDistribucion;
    
    public DistribucionViandas()
    {
    }
    public DistribucionViandas(DateTime fechaContribucion, Heladera heladeraOrigen,
        Heladera heladeraDestino, int cantViandas, MotivoDistribucion motivoDistribucion)
        : base(fechaContribucion)
    {
        _heladeraOrigen = heladeraOrigen;
        _heladeraDestino = heladeraDestino;
        CantViandas = cantViandas;
        _motivoDistribucion = motivoDistribucion;
    }


}