using AccesoAlimentario.API.Domain.Personas;

namespace AccesoAlimentario.API.Domain.Colaboraciones;

public class TarjetaColaboracion : Tarjeta
{
    public TarjetaColaboracion()
    {
    }
    
    public TarjetaColaboracion(string codigo, Persona? propietario) : base(codigo, propietario)
    {
    }
}
