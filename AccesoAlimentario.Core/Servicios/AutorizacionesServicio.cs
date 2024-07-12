using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Servicios;

public class AutorizacionesServicio
{
    public void RegistrarAcceso(Tarjeta tarjeta, TipoAcceso tipoAcceso, Heladera heladera)
    {
        /*var acceso = new AccesoHeladera(tarjeta, DateTime.Now, tipoAcceso, heladera);
        if (!acceso.VerificarValidez())
        {
            throw new Exception("No tiene autorización para acceder a la heladera");
        }
        // TODO: Registrar acceso en la base de datos*/
    }
    
    public void CrearAutorizacion(Tarjeta tarjeta, Heladera heladera)
    {
        
    }
}