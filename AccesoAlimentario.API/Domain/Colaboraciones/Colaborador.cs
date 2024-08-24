using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;
using AccesoAlimentario.API.Domain.Colaboraciones.Suscripciones;
using AccesoAlimentario.API.Domain.Personas;
using AccesoAlimentario.API.Domain.Notificaciones;

namespace AccesoAlimentario.API.Domain.Colaboraciones;

public class Colaborador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    private List<Contribucion> _contribuciones { get; }
    private List<TipoContribucion> _contribucionesPreferidas { get; }
    private float _puntos { get; }
    private TarjetaColaboracion _tarjetaColaboracion { get; set; }
    private List<EventoHeladera> _suscripciones { get; }
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