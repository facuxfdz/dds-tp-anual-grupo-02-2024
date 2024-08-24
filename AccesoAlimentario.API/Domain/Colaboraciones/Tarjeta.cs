using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Personas;

namespace AccesoAlimentario.API.Domain.Colaboraciones;

public abstract class Tarjeta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Codigo { get; set; } = "";
    public Persona? Propietario { get; set; } = null!;

    public Tarjeta()
    {
    }
    
    public Tarjeta(string codigo, Persona? propietario)
    {
        Codigo = codigo;
        Propietario = propietario;
    }
}