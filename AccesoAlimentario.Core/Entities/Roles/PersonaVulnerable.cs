using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class PersonaVulnerable : Rol
{
    public int CantidadDeMenores { get; set; }
    private TarjetaConsumo _tarjeta;
    private DateTime _fechaRegistroSistema;

    public PersonaVulnerable(int id, Persona persona, int cantMenores, TarjetaConsumo tarjeta)
        : base(id,persona)
    {
        CantidadDeMenores = cantMenores;
        _tarjeta = tarjeta;
        _fechaRegistroSistema = DateTime.Now;
    }
}