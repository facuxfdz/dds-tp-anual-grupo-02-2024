using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public abstract class Tarjeta
{
    protected string _codigo;
    protected Persona _propietario;
    protected List<AccesoHeladera> _accesos;
    
    public Tarjeta(string codigo, Persona propietario)
    {
        _codigo = codigo;
        _propietario = propietario;
    }
    
    public void RegistrarConsumo(DateTime fecha, TipoAcceso tipoAcceso, Heladera heladera)
    {
        _accesos.Add(new AccesoHeladera(this, fecha, tipoAcceso, heladera));
    }
}