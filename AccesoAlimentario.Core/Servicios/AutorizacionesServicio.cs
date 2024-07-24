using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Servicios;

public class AutorizacionesServicio(UnitOfWork unitOfWork)
{
    public void RegistrarAcceso(int idTarjeta, TipoAcceso tipoAcceso, Heladera heladera)
    {
        var tarjeta = unitOfWork.TarjetaRepository.GetById(idTarjeta);

        if (tarjeta == null)
        {
            throw new Exception("No se encontró la tarjeta");
        }

        var autorizacion = new AccesoHeladera(tarjeta, heladera, tipoAcceso);
        tarjeta.RegistrarAcceso(autorizacion);

        if (!autorizacion.VerificarValidez())
        {
            throw new Exception("No se puede acceder a la heladera");
        }

        unitOfWork.AccesoHeladeraRepository.Insert(autorizacion);
    }

    public void CrearAutorizacion(int idTarjeta, Heladera heladera)
    {
        var tarjeta = unitOfWork.TarjetaRepository.GetById(idTarjeta);

        if (tarjeta == null)
        {
            throw new Exception("No se encontró la tarjeta");
        }

        if (tarjeta is not TarjetaColaboracion)
        {
            throw new Exception("La tarjeta no es de colaboración");
        }

        var autorizacion = new AutorizacionManipulacionHeladera(
            DateTime.Now.AddDays(1), heladera, tarjeta as TarjetaColaboracion);
        unitOfWork.AutorizacionManipulacionHeladeraRepository.Insert(autorizacion);
    }


}