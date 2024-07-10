using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Personas;

public class PersonaHumana : Persona
{
    private string _apellido;
    private SexoDocumento _sexo;

    public PersonaHumana(string nombre, string apellido, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, List<MedioContacto> mediosDeContacto, List<Rol> roles, SexoDocumento sexo)
        : base(nombre, direccion, documentoIdentidad, mediosDeContacto, roles)
    {
        _apellido = apellido;
        _sexo = sexo;
    }

    public void Actualizar(string nombre, string apellido, Direccion direccion,
        DocumentoIdentidad docId)
    {
        _nombre = nombre;
        _apellido = apellido;
        _direccion = direccion;
        _documentoIdentidad = docId;
    }
    
    public override TipoColaborador ObtenerTipoPersona()
    {
        return TipoColaborador.PersonaHumana;
    }
}