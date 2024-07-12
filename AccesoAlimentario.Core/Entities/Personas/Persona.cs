using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Personas;

public abstract class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public Direccion? Direccion { get; set; }
    public DocumentoIdentidad? DocumentoIdentidad { get; set; }
    // public List<MedioContacto> MediosDeContacto { get; set; }
    protected List<Rol> _roles = new List<Rol>();
    protected DateTime _fechaAlta = DateTime.Now;

    public Persona(int id, string nombre, DocumentoIdentidad documentoIdentidad, Direccion direccion, List<MedioContacto> mediosDeContacto)
    {
        Id = id;
        Nombre = nombre;
        DocumentoIdentidad = documentoIdentidad;
        Direccion = direccion;
        // MediosDeContacto = mediosDeContacto;
    }

    public Persona()
    {
        
    }

}