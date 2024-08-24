using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;

public abstract class Contribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    private DateTime _fecha;
    
    public Contribucion()
    {
    }
    
    public Contribucion(DateTime fecha)
    {
        _fecha = fecha;
    }
}