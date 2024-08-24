using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Personas;

public class Direccion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Calle { get; set; } = "";
    public string Numero { get; set; } = "";
    public string Localidad { get; set; } = "";
    public string? Piso { get; set; } = null;
    public string? Departamento { get; set; } = null;
    public string CodigoPostal { get; set; } = "";

    public Direccion()
    {
    }

    public Direccion(string calle, string numero, string localidad, string codigoPostal)
    {
        Calle = calle;
        Numero = numero;
        Localidad = localidad;
        CodigoPostal = codigoPostal;
    }

    public Direccion(string calle, string numero, string localidad, string codigoPostal, string piso,
        string departamento)
    {
        Calle = calle;
        Numero = numero;
        Localidad = localidad;
        CodigoPostal = codigoPostal;
        Piso = piso;
        Departamento = departamento;
    }
}