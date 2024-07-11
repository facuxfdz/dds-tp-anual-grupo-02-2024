using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public abstract class Tarjeta
{
    protected string _codigo;
    protected Rol _propietario;
    protected List<AccesoHeladera> _accesos = new List<AccesoHeladera>();
    
    public Tarjeta(string codigo, Rol propietario)
    {
        _codigo = codigo;
        _propietario = propietario;
    }
    
    public void RegistrarAcceso(AccesoHeladera acceso)
    {
        _accesos.Add(acceso);
    }
}