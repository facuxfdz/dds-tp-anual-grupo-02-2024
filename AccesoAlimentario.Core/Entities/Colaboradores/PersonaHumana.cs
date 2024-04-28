using AccesoAlimentario.Core.Entities.Direcciones;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public class PersonaHumana
{
    private string _nombre;
    private string _apellido;
    private DateOnly _fechaNacimiento;
    private Direccion _direccion;

    public PersonaHumana(string nombre, string apellido, DateOnly fechaNacimiento, Direccion direccion)
    {
        _nombre = nombre;
        _apellido = apellido;
        _fechaNacimiento = fechaNacimiento;
        _direccion = direccion;
    }

    public void Colaborar()
    {
        
    }

    public void Contactar()
    {
        
    }
}