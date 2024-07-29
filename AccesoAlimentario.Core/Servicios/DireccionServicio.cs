using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Servicios;

public class DireccionServicio(UnitOfWork unitOfWork)
{
    public Direccion Crear(string calle, string numero, string localidad, string codigoPostal)
    {
        var direccion = new Direccion(calle, numero, localidad, codigoPostal);
        try
        {
            unitOfWork.DireccionRepository.Insert(direccion);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception("No se pudo crear la dirección");
        }
        return direccion;
    }
    
    public Direccion? Buscar(string calle, string numero, string localidad, string codigoPostal)
    {
        var direccion = unitOfWork.DireccionRepository.Get(d => d.Calle == calle && d.Numero == numero && d.Localidad == localidad && d.CodigoPostal == codigoPostal).FirstOrDefault();
        return direccion;
    }
}