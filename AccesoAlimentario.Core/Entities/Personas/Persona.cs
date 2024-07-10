using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

namespace AccesoAlimentario.Core.Entities.Personas;

public abstract class Persona
{
    public int Id { get; set; }
    protected string _nombre;
    protected Direccion? _direccion;
    protected DocumentoIdentidad? _documentoIdentidad;
    
    public Persona()
    {
    }
    public Persona(int id, string nombre, Direccion? direccion, DocumentoIdentidad? documentoIdentidad)
    {
        Id = id;
        _nombre = nombre;
        _direccion = direccion;
        _documentoIdentidad = documentoIdentidad;
    }
}