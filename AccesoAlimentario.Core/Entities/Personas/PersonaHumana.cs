using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;

namespace AccesoAlimentario.Core.Entities.Personas;

public class PersonaHumana : Persona
{
    public string Apellido { get; set; } = string.Empty;
    public SexoDocumento Sexo { get; set; } = SexoDocumento.Otro;
    
    public PersonaHumana()
    {
    }

    public PersonaHumana(string nombre, string apellido, List<MedioContacto> medioDeContacto, Direccion direccion,
        DocumentoIdentidad documentoIdentidad, SexoDocumento sexo) : base(nombre, medioDeContacto, direccion, documentoIdentidad)
    {
        Apellido = apellido;
        Sexo = sexo;
    }
    
    
}