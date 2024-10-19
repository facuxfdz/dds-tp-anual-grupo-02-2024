using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Personas;

public abstract class Persona
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = string.Empty;
    public Direccion? Direccion { get; set; } = null;
    public DocumentoIdentidad? DocumentoIdentidad { get; set; } = null;

    public List<MedioContacto> MediosDeContacto { get; set; } = [];

    public List<Rol> Roles { get; set; } = [];
    public DateTime FechaAlta { get; set; } = DateTime.Now;

    public Persona()
    {
    }

    public Persona(string nombre, List<MedioContacto> mediosDeContacto)
    {
        Nombre = nombre;
        MediosDeContacto = mediosDeContacto;
    }

    public Persona(string nombre, List<MedioContacto> medioDeContacto, Direccion direccion,
        DocumentoIdentidad documentoIdentidad)
    {
        Nombre = nombre;
        MediosDeContacto = medioDeContacto;
        Direccion = direccion;
        DocumentoIdentidad = documentoIdentidad;
    }

    public void AgregarRol(Rol rol)
    {
        Roles.Add(rol);
    }

    public void EnviarNotificacion(Notificacion notificacion)
    {
        var medio = MediosDeContacto.Where(m => m.Preferida).FirstOrDefault();
        if (medio != null)
        {
            medio.Enviar(notificacion);
        }
    }
}