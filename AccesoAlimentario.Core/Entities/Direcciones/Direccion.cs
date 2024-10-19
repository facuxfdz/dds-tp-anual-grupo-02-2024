namespace AccesoAlimentario.Core.Entities.Direcciones;

public class Direccion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Localidad { get; set; } = string.Empty;
    public string? Piso { get; set; } = null;
    public string? Departamento { get; set; } = null;
    public string CodigoPostal { get; set; } = string.Empty;

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