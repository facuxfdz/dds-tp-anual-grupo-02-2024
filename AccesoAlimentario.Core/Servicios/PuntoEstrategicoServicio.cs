using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Servicios;

public class PuntoEstrategicoServicio(UnitOfWork unitOfWork)
{
    public void Crear(string nombre, Direccion direccion, float longitud, float latitud)
    {
        if (!LongitudValida(longitud))
        {
            throw new Exception("La longitud es invalida");
        }
        if(!LatitudValida(latitud))
        {
            throw new Exception("La latitud es invalida");
        }
        var puntoEstrategico = unitOfWork.PuntoEstrategicoRepository.Get(p => p.Direccion == direccion).FirstOrDefault();
        if(puntoEstrategico != null)
        {
            throw new Exception("Ya existe un punto estrategico con esa direccion");
        }
        try
        {
            puntoEstrategico = new PuntoEstrategico(nombre, longitud, latitud, direccion);
            unitOfWork.PuntoEstrategicoRepository.Insert(puntoEstrategico);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception("No se pudo crear el punto estrategico");
        }
        
    }
    
    public List<PuntoEstrategico> Listar()
    {
        try
        {
            return unitOfWork.PuntoEstrategicoRepository.Get(includeProperties: "Direccion").ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception("No se pudo listar los puntos estrategicos");
        }
    }
    private bool LongitudValida(float longitud)
    {
        // La longitud válida está entre -180 y 180 grados
        return longitud >= -180 && longitud <= 180;
    }

    private bool LatitudValida(float latitud)
    {
        // La latitud válida está entre -90 y 90 grados
        return latitud >= -90 && latitud <= 90;
    }
}