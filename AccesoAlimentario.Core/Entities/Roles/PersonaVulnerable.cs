using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class PersonaVulnerable : Rol
{
    private DateTime _fechaRegistroSistema;
    private int _cantidadDeMenores;
    private TarjetaConsumo _tarjeta;

    public PersonaVulnerable(Persona persona, int cantidadDeMenores) 
        : base(persona)
    {
        _fechaRegistroSistema = DateTime.Now;
        _cantidadDeMenores = cantidadDeMenores;
    }

    public void Actualizar(int cantMenores)
    {
        _cantidadDeMenores = cantMenores;
    }
}