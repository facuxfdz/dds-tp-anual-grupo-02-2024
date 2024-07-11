using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class Colaborador : Rol
{
    protected List<TipoContribucion> _contribucionesPreferidas;
    protected List<FormaContribucion> _contribucionesRealizadas;
    protected List<Suscripcion> _suscripciones;
    protected float Puntos { get; set; }
    protected TarjetaColaboracion? _tarjetaColaboracion = null;

    public Colaborador(Persona persona, List<TipoContribucion> contribucionesPreferidas)
        : base(persona)
    {
        _contribucionesPreferidas = contribucionesPreferidas;
        _contribucionesRealizadas = new List<FormaContribucion>();
        _suscripciones = new List<Suscripcion>();
        Puntos = 0;
    }

    public void AgregarContribucion(FormaContribucion contribucion)
    {
        _contribucionesRealizadas.Add(contribucion);
    }

    public float ObtenerPuntos()
    {
        return Puntos;
    }

    public void DescontarPuntos(float puntos)
    {
        Puntos -= puntos;
    }

    public void AgregarPuntos(float puntos)
    {
        Puntos += puntos;
    }

    public void AgregarSubscripcion(Suscripcion suscripcion)
    {
        _suscripciones.Add(suscripcion);
    }

    public void EliminarSubscripcion(Suscripcion suscripcion)
    {
        _suscripciones.Remove(suscripcion);
    }

    public void AsignarTarjeta(TarjetaColaboracion tarjeta)
    {
        _tarjetaColaboracion = tarjeta;
    }

}