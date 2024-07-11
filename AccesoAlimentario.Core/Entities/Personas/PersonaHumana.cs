using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;

namespace AccesoAlimentario.Core.Entities.Personas;

public class PersonaHumana : Persona
{
    public string Apellido { get; set; }
    public SexoDocumento Sexo {get; set; }
    
    public PersonaHumana() : base()
    {
    }

    public PersonaHumana(string nombre, string apellido, DocumentoIdentidad documentoIdentidad, Direccion direccion, List<MedioContacto> mediosDeContacto, SexoDocumento sexo)
        : base(nombre, documentoIdentidad, direccion, mediosDeContacto)
    {
        Apellido = apellido;
        Sexo = sexo;
    }
    
}