using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class PersonaVulnerable : Rol
{
    public int CantidadDeMenores { get; set; } = 0;
    public virtual TarjetaConsumo Tarjeta { get; set; } = null!;
    public DateTime FechaRegistroSistema { get; set; } = DateTime.UtcNow;
    
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