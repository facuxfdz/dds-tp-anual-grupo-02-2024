using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Beneficiarios;

public class PersonaVulnerable
{
    private string _nombre;
    private DateOnly _fechaNacimiento;
    private DateTime _fechaRegistroSistema;
    private Direccion? _direccion;
    private TipoDocumento _tipoDocumento;
    private int _nroDocumento;
    private int _cantidadDeMenores;

    public PersonaVulnerable(string nombre, DateOnly fechaNacimiento, DateTime fechaRegistroSistema,
        Direccion direccion, TipoDocumento tipoDocumento, int nroDocumento, int cantidadDeMenores)
    {
        _nombre = nombre;
        _fechaNacimiento = fechaNacimiento;
        _fechaRegistroSistema = fechaRegistroSistema;
        _direccion = direccion;
        _tipoDocumento = tipoDocumento;
        _nroDocumento = nroDocumento;
        _cantidadDeMenores = cantidadDeMenores;
    }

    public void ActualizarDireccion(Direccion direccion)
    {
        _direccion = direccion;
    }
    
    public void ActualizarDocumento(TipoDocumento tipoDocumento, int nroDocumento)
    {
        _tipoDocumento = tipoDocumento;
        _cantidadDeMenores = nroDocumento;
    }
    

}