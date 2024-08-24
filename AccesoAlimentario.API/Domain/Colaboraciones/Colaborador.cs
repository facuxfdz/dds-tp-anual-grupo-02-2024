using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.UseCases.Notificaciones;

namespace AccesoAlimentario.API.Domain.Colaboraciones;

public class Colaborador
{
    private List<Contribucion> _contribuciones { get; }
    private List<TipoContribucion> _contribucionesPreferidas { get; }
    private float _puntos { get; }
    private TarjetaColaboracion _tarjetaColaboracion { get; set; }
    private List<Suscripciones.Suscripciones> _suscripciones { get; }
    private List<CanalNotificacion> _canalesNotificacion { get; }
    private Persona _persona;
    public Colaborador(){}
    
    public Colaborador(Persona persona)
    {
        _persona = persona;
    }
    
    public void AgregarTarjeta(TarjetaColaboracion tarjeta)
    {
        _tarjetaColaboracion = tarjeta;
    }
}