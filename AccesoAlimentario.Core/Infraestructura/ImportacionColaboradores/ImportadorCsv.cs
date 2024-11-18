using System.Globalization;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.ImportacionMasiva;
using CsvHelper;

namespace AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;

public class ImportadorCsv : FormaImportacion
{
    private readonly ValidadorImportacionMasiva _validador = new();

    public override List<Colaborador> ImportarColaboradores(Stream fileStream)
    {
        var colaboraciones = LeerCsv(fileStream);
        var colaboracionesValidas = colaboraciones.Where(c =>
            _validador.Validar(c.TipoDoc, c.Documento, c.Nombre, c.Apellido, c.Mail, c.FechaColaboracion,
                c.FormaColaboracion, c.Cantidad)).ToList();

        var colaboradores = new List<Colaborador>();
        var colaboracionesPorColaborador = colaboracionesValidas.GroupBy(c => new { c.TipoDoc, c.Documento });

        foreach (var colaborador in colaboracionesPorColaborador)
        {
            var c = colaborador.ToList().Select(datos => Parsear(datos)).ToList();
            var col = c.FirstOrDefault();
            if (col == null) continue;
            c.RemoveAt(0);
            foreach (var x in c)
            {
                x.ContribucionesRealizadas.ForEach(col.AgregarContribucion);
            }
            colaboradores.Add(col);
        }

        return colaboradores;
    }

    private static List<DatosColaboracion> LeerCsv(Stream fileStream)
    {
        List<DatosColaboracion> colaboraciones;

        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<DatosColaboracion>();
        colaboraciones = records.ToList();
        return colaboraciones;
    }

    private static Colaborador Parsear(DatosColaboracion datos)
    {
        var tipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), datos.TipoDoc);
        var documento = new DocumentoIdentidad(tipoDoc, datos.Documento, DateOnly.MinValue);
        var personaHumana = new PersonaHumana(datos.Nombre, datos.Apellido, new List<MedioContacto>()
        {
            new Email(true, datos.Mail)
        }, null, documento, SexoDocumento.Otro);
        var colaborador = new Colaborador(personaHumana, []);
        var usuario = new UsuarioSistema(personaHumana, datos.Mail, CrearPassword());
        personaHumana.AgregarRol(usuario);
        personaHumana.AgregarRol(colaborador);
        var tipoContribucion = (TipoContribucion)Enum.Parse(typeof(TipoContribucion), datos.FormaColaboracion);
        var contribuciones = new List<FormaContribucion>();
        var appSettings = AppSettings.Instance;

        var date = DateTime.ParseExact(datos.FechaColaboracion, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (tipoContribucion)
        {
            case TipoContribucion.DINERO:
            {
                contribuciones.Add(new DonacionMonetaria(
                    date,
                    datos.Cantidad, 0));
                colaborador.AgregarPuntos(appSettings.PesoDonadosCoef * datos.Cantidad);
                break;
            }
            case TipoContribucion.DONACION_VIANDAS:
            {
                for (var i = 0; i < datos.Cantidad; i++)
                {
                    contribuciones.Add(new DonacionVianda(date, null, null));
                    colaborador.AgregarPuntos(appSettings.ViandasDonadasCoef * 1);
                }

                break;
            }
            case TipoContribucion.REDISTRIBUCION_VIANDAS:
            {
                contribuciones.Add(new DistribucionViandas(date, null, null,
                    datos.Cantidad, MotivoDistribucion.Desperfecto));
                colaborador.AgregarPuntos(appSettings.ViandasDistribuidasCoef * datos.Cantidad);
                break;
            }
            case TipoContribucion.ENTREGA_TARJETAS:
            {
                for (var i = 0; i < datos.Cantidad; i++)
                {
                    contribuciones.Add(new RegistroPersonaVulnerable(date, null));
                    colaborador.AgregarPuntos(appSettings.TarjetasRepartidasCoef * 1);
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        // agregar contribuciones al colaborador
        foreach (var contribucion in contribuciones)
        {
            colaborador.AgregarContribucion(contribucion);
        }

        return colaborador;
    }

    private static string CrearPassword()
    {
        const int length = 16;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}