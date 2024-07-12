using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Servicios;

public class ColaboradoresServicio {
    //TODO
    public void Crear(Persona persona, List<TipoContribucion> tipoContribuciones) {
    }
    public void Eliminar(Colaborador colaborador) {
    }
    public void Modificar(Colaborador colaborador, List<TipoContribucion> tiposContribuciones){   
    }
    public List<Colaborador> ObtenerTodos() 
    {
        //TODO
        throw new NotImplementedException();
    }
    public Colaborador Buscar(Persona persona)
    {
        //TODO
        throw new NotImplementedException();
    }
    public void AsignarTarjeta(Colaborador colaborador, Tarjeta tarjeta) {
    }
    public int ObtenerPuntos(Colaborador colaborador) {
        //TODO
        throw new NotImplementedException();
    }
    public void SuscribirseAHeladera(Heladera heladera, Suscripcion suscipcion){
    }
    public void DesuscribirseAHeladera(Suscripcion suscipcion){
    }
}