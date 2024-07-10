using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Personas;

public abstract class Persona
{
    protected string _nombre;
    protected Direccion? _direccion;
    protected DocumentoIdentidad? _documentoIdentidad;
    public List<MedioContacto> _mediosDeContacto { get; private set; }
    protected List<Rol> _roles;
    protected DateTime _fechaAlta;
    
    public Persona(string nombre, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, List<MedioContacto> mediosDeContacto, List<Rol> roles)
    {
        _nombre = nombre;
        _direccion = direccion;
        _documentoIdentidad = documentoIdentidad;
        _mediosDeContacto = mediosDeContacto;
        _roles = roles;
        _fechaAlta = DateTime.Now;
    }

    public abstract TipoColaborador ObtenerTipoPersona();

}