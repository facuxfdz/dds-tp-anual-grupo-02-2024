using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Personas;

public abstract class Persona
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Nombre { get; set; } = "";
    public Direccion? Direccion { get; set; } = null;
    public DocumentoIdentidad? DocumentoIdentidad { get; set; } = null;
    
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    
    public Persona()
    {
    }
    
    public Persona(string nombre, Direccion direccion, DocumentoIdentidad documentoIdentidad)
    {
        Nombre = nombre;
        Direccion = direccion;
        DocumentoIdentidad = documentoIdentidad;
    }
}