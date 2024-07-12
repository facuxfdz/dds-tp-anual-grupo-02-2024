using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Tarjetas;

public abstract class Tarjeta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Codigo { get; set; } = "";
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
}