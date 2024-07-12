using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class Colaborador : Rol
{
    public List<TipoContribucion> ContribucionesPreferidas { get; set; }
    protected List<FormaContribucion> _contribucionesRealizadas;
    protected List<Suscripcion> _suscripciones;
    public float Puntos { get; set; }
    public TarjetaColaboracion? TarjetaColaboracion = null;

    public Colaborador(int id, Persona persona, List<TipoContribucion> contribucionesPreferidas)
        : base(id, persona)
    {
        ContribucionesPreferidas = contribucionesPreferidas;
        _contribucionesRealizadas = new List<FormaContribucion>();
        _suscripciones = new List<Suscripcion>();
        Puntos = 0;
    }

    public void AgregarContribucion(FormaContribucion contribucion)
    {
        _contribucionesRealizadas.Add(contribucion);
    }

    public void EliminarSubscripcion(Suscripcion suscripcion)
    {
        _suscripciones.Remove(suscripcion);
    }

    public void AsignarTarjeta(TarjetaColaboracion tarjeta)
    {
        TarjetaColaboracion = tarjeta;
    }

}