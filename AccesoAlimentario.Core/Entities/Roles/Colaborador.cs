using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.SuscripcionesColaboradores;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class Colaborador : Rol
{
    public List<TipoContribucion> ContribucionesPreferidas { get; private set; } = [];
    public List<FormaContribucion> ContribucionesRealizadas { get; private set; } = [];
    public List<Suscripcion> Suscripciones { get; private set; } = [];
    public float Puntos { get; set; } = 0;
    public TarjetaColaboracion? TarjetaColaboracion = null;

    public Colaborador()
    {
    }
    
    public Colaborador(Persona persona, List<TipoContribucion> contribucionesPreferidas) : base(persona)
    {
        ContribucionesPreferidas = contribucionesPreferidas;
    }

    public void AgregarContribucion(FormaContribucion contribucion)
    {
        ContribucionesRealizadas.Add(contribucion);
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
        Suscripciones.Add(suscripcion);
    }

    public void EliminarSubscripcion(Suscripcion suscripcion)
    {
        Suscripciones.Remove(suscripcion);
    }

    public void AsignarTarjeta(TarjetaColaboracion tarjeta)
    {
        TarjetaColaboracion = tarjeta;
    }
}