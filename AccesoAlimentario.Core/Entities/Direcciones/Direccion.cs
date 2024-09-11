using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Direcciones;

public class Direccion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Calle { get; private set; } = "";
    public string Numero { get; private set; } = "";
    public string Localidad { get; private set; } = "";
    public string? Piso { get; private set; } = null;
    public string? Departamento { get; private set; } = null;
    public string CodigoPostal { get; private set; } = "";

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