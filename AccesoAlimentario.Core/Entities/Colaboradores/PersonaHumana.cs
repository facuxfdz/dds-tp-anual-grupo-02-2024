using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public class PersonaHumana
{
    private string _nombre;
    private string _apellido;
    //private List<MedioDeContacto> _mediosDeContacto;
    //private DateOffSet _fechaNacimiento;
    private Direccion _direccion;

    public PersonaHumana(string nombre, string apellido)
    {
        _nombre = nombre;
        _apellido = apellido;
    }
}