using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas.Colaboradores;

public class PersonaHumana : Colaborador
{
    public string Apellido { get; set; }
    public DateOnly? FechaNacimiento { get; set; }

    public PersonaHumana()
    {
    }

    public PersonaHumana(int id, string nombre, string apellido, DateOnly? fechaNacimiento, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, Usuario usuario,
        List<TipoContribucion> tiposDeContribucionesElegidas)
        : base(id, nombre, direccion, documentoIdentidad, usuario, tiposDeContribucionesElegidas)
    {
        Id = id;
        Apellido = apellido;
        FechaNacimiento = fechaNacimiento;
    }

    public void Actualizar(string nombre, string apellido, DateOnly fechaNac, Direccion direccion,
        DocumentoIdentidad docId)
    {
        _nombre = nombre;
        Apellido = apellido;
        FechaNacimiento = fechaNac;
        _direccion = direccion;
        _documentoIdentidad = docId;
    }
}