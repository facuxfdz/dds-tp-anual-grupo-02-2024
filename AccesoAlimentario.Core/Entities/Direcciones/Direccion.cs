namespace AccesoAlimentario.Core.Entities.Direcciones;

public class Direccion
{
    public int Id { get; private set; }
    public string Calle { get; private set; }
    public string Numero { get; private set; }
    public string Localidad { get; private set; }
    public string? Piso { get; private set; }
    public string? Departamento { get; private set; }
    public string CodigoPostal { get; private set; }
    
    public Direccion()
    {
    }
    public Direccion(int id, string calle, string numero, string localidad, string codigoPostal, string? piso = null,
        string? departamento = null)
    {
        Id = id;
        Calle = calle;
        Numero = numero;
        Localidad = localidad;
        Piso = piso;
        Departamento = departamento;
        CodigoPostal = codigoPostal;
    }

    public void Actualizar(string calle, string numero, string localidad, string codigoPostal, string? piso = null,
        string? departamento = null)
    {
        Calle = calle;
        Numero = numero;
        Localidad = localidad;
        Piso = piso;
        Departamento = departamento;
        CodigoPostal = codigoPostal;
    }
}