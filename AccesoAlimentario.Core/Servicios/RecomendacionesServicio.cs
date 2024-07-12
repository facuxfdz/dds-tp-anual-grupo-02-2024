using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Infraestructura.RecomendacionUbicacionHeladeras;

namespace AccesoAlimentario.Core.Servicios;

public class RecomendacionesServicio
{
    public ICollection<PuntoEstrategico> ObtenerPuntosRecomendados(float latitud, float longitud, float radio)
    {
        var consultoraExternaApi = new ConsultoraExternaApi();
        return consultoraExternaApi.GetRecomendacion(latitud, longitud, radio);
    }
}