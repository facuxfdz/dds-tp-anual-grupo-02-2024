using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;

namespace AccesoAlimentario.Core.Entities.Personas;

public class PersonaJuridica : Persona
{
    public string RazonSocial { get; set;}
    public TipoJuridico Tipo { get; set; } = TipoJuridico.Gubernamental;
    public string Rubro { get; set; } = "";
    
    public PersonaJuridica()
    {
    }
    
    public PersonaJuridica(string nombre, string razonSocial,TipoJuridico tipo, string rubro, List<MedioContacto> medioDeContacto,
        Direccion direccion,
        DocumentoIdentidad documentoIdentidad) : base(nombre, medioDeContacto, direccion, documentoIdentidad)
    {
        RazonSocial = razonSocial;
        Tipo = tipo;
        Rubro = rubro;
    }
}