using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public class TarjetaColaboracion : Tarjeta
{
    public TarjetaColaboracion(string codigo, Persona propietario) //TODO restringir propietario a tipo Colaborador
        : base(codigo, propietario)
    {
    }
    
}