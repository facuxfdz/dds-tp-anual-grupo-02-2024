
namespace AccesoAlimentario.API.Domain.Personas;

public class PersonaJuridica : Persona
{
    public string RazonSocial { get; set;}
    public TipoJuridico Tipo { get; set; } = TipoJuridico.Gubernamental;
    public string Rubro { get; set; } = "";
    
    public PersonaJuridica()
    {
    }
    
    public PersonaJuridica(string nombre, string razonSocial,TipoJuridico tipo, string rubro,
        Direccion direccion,
        DocumentoIdentidad documentoIdentidad) : base(nombre, direccion, documentoIdentidad)
    {
        RazonSocial = razonSocial;
        Tipo = tipo;
        Rubro = rubro;
    }
}