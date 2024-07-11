using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class PersonaVulnerable : Rol
{
    private int _cantidadDeMenores = 0;
    private TarjetaConsumo _tarjeta;
    private DateTime _fechaRegistroSistema;

    public PersonaVulnerable(Persona persona, int cantMenores, TarjetaConsumo tarjeta)
        : base(persona)
    {
        _cantidadDeMenores = cantMenores;
        _tarjeta = tarjeta;
        _fechaRegistroSistema = DateTime.Now;
    }
    public void Actualizar(int cantMenores)
    {
        _cantidadDeMenores = cantMenores;
    }
}