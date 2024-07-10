using System.Runtime.InteropServices.JavaScript;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Autorizaciones;

public class AccesoHeladera
{
    private Tarjeta _tarjeta;
    private DateTime _fechaAcceso;
    private TipoAcceso _tipoAcceso;
    private Heladera _heladera;
    private AutorizacionManipulacionHeladera? _autorizacion;
    
    public AccesoHeladera(Tarjeta tarjeta, DateTime fechaAcceso, TipoAcceso tipoAcceso, Heladera heladera)
    {
        _tarjeta = tarjeta;
        _fechaAcceso = fechaAcceso;
        _tipoAcceso = tipoAcceso;
        _heladera = heladera;
    }
}