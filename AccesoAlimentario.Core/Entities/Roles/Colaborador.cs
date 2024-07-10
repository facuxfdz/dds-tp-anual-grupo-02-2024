using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Scoring;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class Colaborador : Rol
{
    public int Id { get; set; }
    protected List<TipoContribucion> _tiposDeContribucionesElegidas;
    protected List<FormaContribucion> _contribucionesRealizadas;
    protected List<Suscripcion> _suscripciones;
    protected float _puntos;
    protected TarjetaColaboracion _tarjetaColaboracion;

    public Colaborador(Persona persona, List<TipoContribucion> tiposDeContribucionesElegidas, List<Suscripcion> suscripciones, TarjetaColaboracion tarjetaColaboracion)
        : base(persona)
    {
        _tiposDeContribucionesElegidas = tiposDeContribucionesElegidas;
        _contribucionesRealizadas = new List<FormaContribucion>();
        _puntos = 0;
    }

    public void Colaborar(FormaContribucion formaContribucion)
    {
        if (formaContribucion.EsValido(this))
        {
            _contribucionesRealizadas.Add(formaContribucion);
            var score = new Score();
            score.Calcular(this, formaContribucion);
        }
    }

    public void Contactar(Notificacion notificacion)
    {
        _persona._mediosDeContacto.FirstOrDefault()?.Enviar(notificacion);
    }

    public float ObtenerPuntos() => _puntos;

    public void DescontarPuntos(float valor)
    {
        _puntos -= valor;
    }

    public void AgregarPuntos(float valor)
    {
        _puntos += valor;
    }
}