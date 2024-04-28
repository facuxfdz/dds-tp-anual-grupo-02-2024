using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DistribucionVianda
{
    private Heladera _heladeraOrigen;
    private Heladera _heladeraDestino;
    private int _cantViandas;
    private MotivoDistribucion _motivoDistribucion;

    public DistribucionVianda(Heladera heladeraOrigen, Heladera heladeraDestino, int cantViandas,
        MotivoDistribucion motivoDistribucion)
    {
        _heladeraOrigen = heladeraOrigen;
        _heladeraDestino = heladeraDestino;
        _cantViandas = cantViandas;
        _motivoDistribucion = motivoDistribucion;
    }

    public void Colaborar()
    {
        
    }

}