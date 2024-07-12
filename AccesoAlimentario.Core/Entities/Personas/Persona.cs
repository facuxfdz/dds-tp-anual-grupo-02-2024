using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Personas;

public abstract class Persona
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Nombre { get; set; } = "";
    public Direccion? Direccion { get; set; } = null;
    public DocumentoIdentidad? DocumentoIdentidad { get; set; } = null;

    public List<MedioContacto> MediosDeContacto { get; set; } = [];

    public List<Rol> Roles { get; set; } = [];
    public DateTime FechaAlta { get; set; } = DateTime.Now;

    public Persona()
    {
    }

    public Persona(string nombre, List<MedioContacto> mediosDeContacto)
    {
        Nombre = nombre;
        MediosDeContacto = mediosDeContacto;
    }

    public Persona(string nombre, List<MedioContacto> medioDeContacto, Direccion direccion,
        DocumentoIdentidad documentoIdentidad)
    {
        Nombre = nombre;
        MediosDeContacto = medioDeContacto;
        Direccion = direccion;
        DocumentoIdentidad = documentoIdentidad;
    }
}