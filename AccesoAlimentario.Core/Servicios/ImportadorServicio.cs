using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Notificaciones;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;

namespace AccesoAlimentario.Core.Servicios;

public class ImportadorServicio(UnitOfWork unitOfWork, PersonasServicio personasServicio, ColaboradoresServicio colaboradoresServicio)
{
    public void Importar(string archivoBase64)
    {
        var importador = new ImportadorCsv();
        using var streamFile = new MemoryStream(Convert.FromBase64String(archivoBase64));
        var colaboradores = importador.ImportarColaboradores(streamFile);
        foreach (var colaborador in colaboradores)
        {
            var persona = colaborador.Persona;
            var existePersona = personasServicio.Buscar(persona.DocumentoIdentidad);
            if (existePersona == null)
            {
                var ph = (PersonaHumana)persona;
                persona = personasServicio.CrearHumana(persona.Nombre, persona.Direccion, persona.DocumentoIdentidad, persona.MediosDeContacto, ph.Apellido, ph.Sexo);
                var usuarioS = (UsuarioSistema)ph.Roles.Find(x => x is UsuarioSistema);
                var builder = new NotificacionUsuarioCreadoBuilder(usuarioS.UserName, usuarioS.Password);
                persona.EnviarNotificacion(builder.CrearNotificacion());
                /*personasServicio.AgregarRol(persona.Id, colaborador);*/
                personasServicio.AgregarRol(persona.Id, usuarioS);
            } else {
                persona = (PersonaHumana)existePersona;
            }
            // TODO: ACA SE DEBERIA VALIDAR SI POSEE EL ROL DE COLABORADOR
            colaboradoresServicio.Crear(persona, []);
            /*colaborador.ContribucionesRealizadas.ForEach(contribucion =>
            {
                switch (contribucion)
                {
                    
                }
            });*/
        }
    }
}