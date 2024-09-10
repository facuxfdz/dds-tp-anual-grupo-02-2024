using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Personas;

namespace AccesoAlimentario.API.Domain.Tecnicos;

public class Tecnico
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    public AreaCobertura AreaCobertura { get; private set; } = null!;
    
    public Persona Persona { get; private set; }
    
    public Tecnico(Persona persona, AreaCobertura areaCobertura)
    {
        Persona = persona;
        AreaCobertura = areaCobertura;
    }
}