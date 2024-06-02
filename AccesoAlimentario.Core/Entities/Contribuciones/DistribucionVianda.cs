using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Interfaces;
using AccesoAlimentario.Core.Interfaces.Validadores;
using AccesoAlimentario.Core.Resources;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DistribucionVianda : FormaContribucion
{
    private Heladera _heladeraOrigen;
    private Heladera _heladeraDestino;
    private int _cantViandas;
    private MotivoDistribucion _motivoDistribucion;


    public DistribucionVianda(Colaborador colaborador, IValidadorContribuciones validadorContribuciones,
        DateTime fechaContribucion, Heladera heladeraOrigen, Heladera heladeraDestino, int cantViandas,
        MotivoDistribucion motivoDistribucion)
        : base(colaborador, validadorContribuciones, fechaContribucion)
    {
        _heladeraOrigen = heladeraOrigen;
        _heladeraDestino = heladeraDestino;
        _cantViandas = cantViandas;
        _motivoDistribucion = motivoDistribucion;
    }


    public override float CalcularPuntos()
    {
        return Config.ViandasDistribuidasCoef * _cantViandas;
    }
}