using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;

namespace AccesoAlimentario.Core.Entities.Beneficiarios;

public class PersonaVulnerable
{
    private string _nombre;
    private DateOnly _fechaNacimiento;
    private DateTime _fechaRegistroSistema;
    private Direccion? _direccion;
    private DocumentoIdentidad? _documentoIdentidad;
    private int _cantidadDeMenores;

    public PersonaVulnerable(string nombre, DateOnly fechaNacimiento, DateTime fechaRegistroSistema,
        Direccion? direccion, DocumentoIdentidad? documentoIdentidad, int cantidadDeMenores)
    {
        _nombre = nombre;
        _fechaNacimiento = fechaNacimiento;
        _fechaRegistroSistema = fechaRegistroSistema;
        _direccion = direccion;
        _documentoIdentidad = documentoIdentidad;
        _cantidadDeMenores = cantidadDeMenores;
    }

    public void ActualizarDireccion(Direccion direccion)
    {
        _direccion = direccion;
    }

    public void ActualizarDocumento(DocumentoIdentidad documentoIdentidad)
    {
        _documentoIdentidad = documentoIdentidad;
    }
}