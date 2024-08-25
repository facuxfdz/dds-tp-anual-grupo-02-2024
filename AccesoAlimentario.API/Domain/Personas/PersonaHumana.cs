
using AccesoAlimentario.API.Domain.Personas;

public class PersonaHumana : Persona
{
    public string Apellido { get; set; } = "";
    public SexoDocumento? Sexo { get; set; } = SexoDocumento.Otro;
    
    public PersonaHumana()
    {
    }

    public PersonaHumana(string nombre, string apellido, Direccion direccion,
        DocumentoIdentidad documentoIdentidad, SexoDocumento? sexo) : base(nombre, direccion, documentoIdentidad)
    {
        Apellido = apellido;
        Sexo = sexo;
    }
}