namespace AccesoAlimentario.Core.Entities.Direcciones;

public class Direccion
{
    private string _calle;
    private int _numero;
    private string _localidad;
    private int? _piso;
    private int? _departamento;
    private int _codigoPostal;

    public Direccion(string calle, int numero, string localidad, int? piso, int? departamento, int codigoPostal)
    {
        _calle = calle;
        _numero = numero;
        _localidad = localidad;
        _piso = piso;
        _departamento = departamento;
        _codigoPostal = codigoPostal;
    }

    public void Actulizar(string calle, int numero, string localidad, int piso, int departamento, int codigoPostal)
    {
        _calle = calle;
        _numero = numero;
        _localidad = localidad;
        _piso = piso;
        _departamento = departamento;
        _codigoPostal = codigoPostal;
    }
   
}