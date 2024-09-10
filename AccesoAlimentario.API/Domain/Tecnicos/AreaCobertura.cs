using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Tecnicos;

public class AreaCobertura
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public float Latitud { get; set; } = 0f;
    public float Longitud { get; set; } = 0f;
    public float Radio { get; set; } = 0f;
    
    

    public bool EsCercano(float longitud, float latitud)
    {
        var distancia = Math.Sqrt(Math.Pow(longitud - Longitud, 2) + Math.Pow(latitud - Latitud, 2));
        return distancia <= Radio;
    }
}