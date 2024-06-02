using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

namespace AccesoAlimentario.Core.Entities.Personas.Beneficiarios;

public class PersonaVulnerable : Persona
{
    private DateOnly _fechaNacimiento;
    private DateTime _fechaRegistroSistema;
    private int _cantidadDeMenores;

    public PersonaVulnerable(string nombre, DateOnly fechaNacimiento, DateTime fechaRegistroSistema,
        Direccion? direccion, DocumentoIdentidad? documentoIdentidad, int cantidadDeMenores) 
        : base(nombre, direccion, documentoIdentidad)
    {
        _fechaNacimiento = fechaNacimiento;
        _fechaRegistroSistema = fechaRegistroSistema;
        _cantidadDeMenores = cantidadDeMenores;
    }

    public void Actualizar(string nombre, DateOnly fechaNac, Direccion direccion, DocumentoIdentidad docId, int cantMenores)
    {
        _nombre = nombre;
        _fechaNacimiento = fechaNac;
        _direccion = direccion;
        _documentoIdentidad = docId;
        _cantidadDeMenores = cantMenores;
    }
}