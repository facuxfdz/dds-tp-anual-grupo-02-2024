using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public class PersonaJuridica : Colaborador
{
    private string _razonSocial;
    private TipoJuridico _tipo;
    private string _rubro;
    // private List<MedioDeContacto> mediosDeContacto;
    private Heladera heladera;

    public PersonaJuridica(string razonSocial, TipoJuridico tipoJuridico)
    {
        _razonSocial = razonSocial;
        _tipo = tipoJuridico;
    }

}