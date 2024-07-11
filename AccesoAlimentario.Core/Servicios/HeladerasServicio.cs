using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Servicios;

public class HeladerasServicio {
    public void Crear(string nombre, float longi, float lat, Direccion direccion, ModeloHeladera modelo, float temperaturaMinima, float temperaturaMaxima) 
    {
        PuntoEstrategico puntoEstrategico = new PuntoEstrategico(nombre, longi, lat, direccion);
        Heladera heladera = new Heladera(puntoEstrategico, temperaturaMinima, temperaturaMaxima, modelo);
    }
}