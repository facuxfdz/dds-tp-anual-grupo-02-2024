// using AccesoAlimentario.Core.Entities.Direcciones;
// using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
// using AccesoAlimentario.Core.Entities.Heladeras;
// using AccesoAlimentario.Core.Entities.Incidentes;
// using AccesoAlimentario.Core.Entities.MediosContacto;
// using AccesoAlimentario.Core.Entities.Notificaciones;
// using AccesoAlimentario.Core.Entities.Personas;
// using AccesoAlimentario.Core.Entities.Roles;
//
// namespace AccesoAlimentario.Testing;
//
// public class TestNotificaciones
// {
//     private Colaborador _colaborador;
//     private Persona _persona;
//     private List<MedioContacto> _medioContacto;
//
//     private Alerta _alerta;
//     private TipoAlerta _tipoAlerta;
//     private FallaTecnica _fallaTecnica;
//     private string? _descripcionFallaTecnica;
//     private string? _fotoFallaTecnica;
//
//     private Heladera _heladera;
//     private ModeloHeladera _modeloHeladera;
//     private PuntoEstrategico _puntoEstrategico;
//     private Direccion _direccion;
//     private Vianda _vianda;
//     private int _capacidadLimiteHeladera;
//     private int _cantidadViandasHeladera;
//     private int _cantidadParaQueSeLleneHeladera;
//
//     private UsuarioSistema _usuarioSistema;
//     private DocumentoIdentidad _documentoIdentidad;
//
//
//     [SetUp]
//     public void Setup()
//     {
//         _alerta = new Alerta(TipoAlerta.Conexion);
//         _tipoAlerta = _alerta.Tipo;
//
//         _medioContacto = new List<MedioContacto>();
//         _documentoIdentidad = new DocumentoIdentidad();
//         _colaborador = new Colaborador();
//
//
//         _fallaTecnica = new FallaTecnica(_colaborador, "Falla tecnica en la heladera", "Foto de la falla tecnica");
//         _descripcionFallaTecnica = _fallaTecnica.Descripcion;
//         _fotoFallaTecnica = _fallaTecnica.Foto;
//
//         _vianda = new Vianda();
//         _modeloHeladera = new ModeloHeladera(10, 2, 8);
//         _direccion = new Direccion("Calle falsa 123", "Springfield", "Capital Federal", "Argentina");
//         _puntoEstrategico = new PuntoEstrategico("Punto estrategico", 0, 0, _direccion);
//         _heladera = new Heladera(_puntoEstrategico, 0, 9, _modeloHeladera);
//         _capacidadLimiteHeladera = _heladera.Modelo.Capacidad;
//         _heladera.IngresarVianda(_vianda);
//         _cantidadViandasHeladera = _heladera.Viandas.Count;
//         _cantidadParaQueSeLleneHeladera = _capacidadLimiteHeladera - _cantidadViandasHeladera;
//
//         _persona = new PersonaHumana("Nico", "Perez", _medioContacto, _direccion, _documentoIdentidad,
//             SexoDocumento.Masculino);
//         _usuarioSistema = new UsuarioSistema(_persona, "Nico", "1234");
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacion_True()
//     {
//         var notificacion = new Notificacion("Mensaje de prueba", "Asunto de prueba");
//
//         Assert.That(notificacion.Asunto, Is.EqualTo("Mensaje de prueba"));
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacionAlerta_True()
//     {
//         var notificacion = new NotificacionIncidenteAlertaBuilder(_tipoAlerta);
//         var notificacionCreada = notificacion.CrearNotificacion();
//
//         Assert.That(notificacionCreada.Asunto, Is.EqualTo("Acceso Alimentario: Alerta de Sensor"));
//         Assert.That(notificacionCreada.Mensaje,
//             Is.EqualTo(
//                 "Un sensor ha detectado una alerta de tipo Conexion. Por favor, acudir al lugar en la brevedad."));
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacionFallaTecnica_True()
//     {
//         var notificacion = new NotificacionIncidenteFallaTecnicaBuilder(_descripcionFallaTecnica, _fotoFallaTecnica);
//         var notificacionCreada = notificacion.CrearNotificacion();
//
//         Assert.That(notificacionCreada.Asunto,
//             Is.EqualTo("Acceso Alimentario: Un usuario ha reportado una Falla Técnica"));
//         Assert.IsTrue(notificacionCreada.Mensaje.Contains(
//             "Un usuario ha reportado una falla tecnica en una heladera. Por favor, acudir al lugar en la brevedad."));
//         Assert.IsTrue(notificacionCreada.Mensaje.Contains("Descripción: Falla tecnica en la heladera"));
//         Assert.IsTrue(notificacionCreada.Mensaje.Contains("Foto: Foto de la falla tecnica"));
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacionFallaTecnica_SinFoto()
//     {
//         var notificacion = new NotificacionIncidenteFallaTecnicaBuilder(_descripcionFallaTecnica, null);
//         var notificacionCreada = notificacion.CrearNotificacion();
//
//         Assert.IsTrue(notificacionCreada.Mensaje.Contains("Descripción: Falla tecnica en la heladera"));
//         Assert.IsFalse(notificacionCreada.Mensaje.Contains("Foto"));
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacionFallaTecnica_SinDescripcion()
//     {
//         var notificacion = new NotificacionIncidenteFallaTecnicaBuilder(null, _fotoFallaTecnica);
//         var notificacionCreada = notificacion.CrearNotificacion();
//
//         Assert.IsTrue(notificacionCreada.Mensaje.Contains("Foto: Foto de la falla tecnica"));
//         Assert.IsFalse(notificacionCreada.Mensaje.Contains("Descripción"));
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacionExcedente()
//     {
//         var notificacion = new NotificacionExcedenteBuilder(_cantidadParaQueSeLleneHeladera);
//         var notificacionCreada = notificacion.CrearNotificacion();
//
//         Assert.That(notificacionCreada.Asunto, Is.EqualTo("Acceso Alimentario: Hay un exceso de viandas"));
//         Assert.IsTrue(notificacionCreada.Mensaje.Contains(
//             "Faltan 9 viandas para que se llene la heladera. Por favor, distribuir en la brevedad."));
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacionFaltante()
//     {
//         var notificacion = new NotificacionFaltanteBuilder(_cantidadViandasHeladera);
//         var notificacionCreada = notificacion.CrearNotificacion();
//
//         Assert.That(notificacionCreada.Asunto, Is.EqualTo("Acceso Alimentario: Hay un faltante viandas"));
//         Assert.IsTrue(
//             notificacionCreada.Mensaje.Contains(
//                 "Faltan 1 viandas para vaciar la heladera. Por favor, reponer en la brevedad."));
//     }
//
//     [Test]
//     public void Notificacion_CrearNotificacionUsuarioCreado()
//     {
//         var notificacion = new NotificacionUsuarioCreadoBuilder(_usuarioSistema.UserName, _usuarioSistema.Password);
//         var notificacionCreada = notificacion.CrearNotificacion();
//
//         Assert.That(notificacionCreada.Asunto, Is.EqualTo("Acceso Alimentario: Su usuario ha sido creado con exito"));
//         Assert.IsTrue(notificacionCreada.Mensaje.Contains(
//             "Su usuario <b>Nico</b> se ha creado con exito, su contraseña es: <b>1234</b>.<br>No comparta esta información, muchas gracias."));
//     }
// }