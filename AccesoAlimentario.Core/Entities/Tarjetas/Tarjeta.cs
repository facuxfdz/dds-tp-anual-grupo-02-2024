using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public abstract class Tarjeta
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Codigo { get; set; } = string.Empty;
    public Rol Propietario { get; set; } = null!;
    public List<AccesoHeladera> Accesos { get; set; } = [];

    public Tarjeta()
    {
    }

    public Tarjeta(string codigo, Rol propietario)
    {
        Codigo = codigo;
        Propietario = propietario;
    }

    public void RegistrarAcceso(AccesoHeladera acceso)
    {
        Accesos.Add(acceso);
    }
    
    public void AsignarPropietario(Rol propietario)
    {
        Propietario = propietario;
    }
}