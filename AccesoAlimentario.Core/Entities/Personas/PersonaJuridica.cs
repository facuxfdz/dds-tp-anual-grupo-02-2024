using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas;

public class PersonaJuridica : Colaborador
{
    private string _razonSocial;
    private TipoJuridico _tipo;
    private string _rubro;

    public PersonaJuridica(string razonSocial, TipoJuridico tipoJuridico, string rubro, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, Usuario usuario) 
        : base(razonSocial, direccion, documentoIdentidad, usuario)
    {
        _razonSocial = razonSocial;
        _tipo = tipoJuridico;
        _rubro = rubro;
    }

    public override void Contactar()
    {
        
    }

}