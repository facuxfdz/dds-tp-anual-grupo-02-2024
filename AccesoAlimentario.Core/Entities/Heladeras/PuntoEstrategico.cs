using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Heladeras;

public class PuntoEstrategico
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Nombre { get; private set; } = null!;
    public float Longitud { get; private set; } = 0;
    public float Latitud { get; private set; } = 0;
    public Direccion Direccion { get; private set; } = null!;

    public PuntoEstrategico()
    {
    }

    public PuntoEstrategico(string nombre, float longitud, float latitud, Direccion direccion)
    {
        Nombre = nombre;
        Longitud = longitud;
        Latitud = latitud;
        Direccion = direccion;
    }

    public void Actualizar(string nombre, float longitud, float latitud, Direccion direccion)
    {
        Nombre = nombre;
        Longitud = longitud;
        Latitud = latitud;
        Direccion = direccion;
    }
}