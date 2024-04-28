namespace AccesoAlimentario.Core.Entities.Persona;

public class PersonaJuridica : Colaborador
{
    private string _razonSocial;
    private TipoJuridico _tipo;
    private string _rubro;
    private List<MedioDeContacto> mediosDeContacto;
    private Heladera heladera;

    public PersonaJuridica(string razonSocial, TipoJuridico tipoJuridico)
    {
        _razonSocial = razonSocial;
        _tipo = tipoJuridico;
    }

}