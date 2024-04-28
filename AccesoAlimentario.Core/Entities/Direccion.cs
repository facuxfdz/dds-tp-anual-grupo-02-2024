namespace AccesoAlimentario.Core.Entities;

public class Direccion
{
    private string _calle;
    private int _numero;
    private string _localidad;
    private int _piso;
    private int _departamento;
    private int _codigoPostal;

    public Direccion(string calle, int numero, string localidad)
    {
        _calle = calle;
        _numero = numero;
        _localidad = localidad;
    }
}