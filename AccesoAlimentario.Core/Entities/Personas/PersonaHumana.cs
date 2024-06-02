using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas;

public class PersonaHumana : Colaborador
{
    private string _apellido;
    private DateOnly? _fechaNacimiento;

    public PersonaHumana(string nombre, string apellido, DateOnly fechaNacimiento, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, Usuario usuario) 
        : base(nombre, direccion, documentoIdentidad, usuario)
    {
        _apellido = apellido;
        _fechaNacimiento = fechaNacimiento;
    }

    public override void Contactar()
    {
        
    }
}