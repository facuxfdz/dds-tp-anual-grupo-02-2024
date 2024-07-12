using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class PersonaVulnerable : Rol
{
    public int CantidadDeMenores { get; private set; } = 0;
    public TarjetaConsumo Tarjeta { get; private set; } = null;
    public DateTime FechaRegistroSistema { get; private set; } = DateTime.Now;
    
    public PersonaVulnerable()
    {
    }
    
    public PersonaVulnerable(Persona persona, int cantidadDeMenores, TarjetaConsumo tarjeta) : base(persona)
    {
        CantidadDeMenores = cantidadDeMenores;
        Tarjeta = tarjeta;
    }

    public void Actualizar(int cantMenores)
    {
        CantidadDeMenores = cantMenores;
    }
}