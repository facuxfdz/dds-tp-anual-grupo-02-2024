namespace AccesoAlimentario.Core.Entities.Direcciones;

public class Direccion
{
    private string _calle;
    private string _numero;
    private string _localidad;
    private string? _piso;
    private string? _departamento;
    private string _codigoPostal;

    public Direccion(string calle, string numero, string localidad, string codigoPostal, string? piso = null,
        string? departamento = null)
    {
        _calle = calle;
        _numero = numero;
        _localidad = localidad;
        _piso = piso;
        _departamento = departamento;
        _codigoPostal = codigoPostal;
    }

    public void Actualizar(string calle, string numero, string localidad, string codigoPostal, string? piso = null,
        string? departamento = null)
    {
        _calle = calle;
        _numero = numero;
        _localidad = localidad;
        _piso = piso;
        _departamento = departamento;
        _codigoPostal = codigoPostal;
    }
}