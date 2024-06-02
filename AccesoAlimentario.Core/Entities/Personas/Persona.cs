using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

namespace AccesoAlimentario.Core.Entities.Personas;

public abstract class Persona
{
    protected string _nombre;
    protected Direccion? _direccion;
    protected DocumentoIdentidad? _documentoIdentidad;
    
    public Persona(string nombre, Direccion? direccion, DocumentoIdentidad? documentoIdentidad)
    {
        _nombre = nombre;
        _direccion = direccion;
        _documentoIdentidad = documentoIdentidad;
    }
    
}