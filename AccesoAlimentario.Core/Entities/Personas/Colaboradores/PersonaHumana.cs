using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas.Colaboradores;

public class PersonaHumana : Colaborador
{
    private string _apellido;
    private DateOnly? _fechaNacimiento;

    public PersonaHumana(string nombre, string apellido, DateOnly? fechaNacimiento, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, Usuario usuario,
        List<TipoContribucion> tiposDeContribucionesElegidas)
        : base(nombre, direccion, documentoIdentidad, usuario, tiposDeContribucionesElegidas)
    {
        _apellido = apellido;
        _fechaNacimiento = fechaNacimiento;
    }

    public void Actualizar(string nombre, string apellido, DateOnly fechaNac, Direccion direccion,
        DocumentoIdentidad docId)
    {
        _nombre = nombre;
        _apellido = apellido;
        _fechaNacimiento = fechaNac;
        _direccion = direccion;
        _documentoIdentidad = docId;
    }
}