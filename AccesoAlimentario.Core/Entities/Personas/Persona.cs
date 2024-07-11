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
    protected List<Rol> _roles = new List<Rol>();
    protected DateTime _fechaAlta = DateTime.Now;

    public Persona(string nombre, DocumentoIdentidad documentoIdentidad, Direccion direccion, List<MedioContacto> mediosDeContacto)
    {
        _nombre = nombre;
        _documentoIdentidad = documentoIdentidad;
        _direccion = direccion;
        _mediosDeContacto = mediosDeContacto;
    }

}