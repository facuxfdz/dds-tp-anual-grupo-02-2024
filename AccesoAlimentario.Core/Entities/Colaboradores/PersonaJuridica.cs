using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public class PersonaJuridica
{
    private string _razonSocial;
    private TipoJuridico _tipo;
    private string _rubro;

    public PersonaJuridica(string razonSocial, TipoJuridico tipoJuridico, string rubro)
    {
        _razonSocial = razonSocial;
        _tipo = tipoJuridico;
        _rubro = rubro;
    }

    public void Colaborar()
    {
        
    }

    public void Contactar()
    {
        
    }

}