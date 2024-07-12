using System.Globalization;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Validadores.ImportacionMasiva;
using CsvHelper;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ImportadorCsv : FormaImportacion
{
    private readonly ValidadorImportacionMasiva _validador = new();
        
    private static int GenerarId()
    {
        var randomId = new Random();
        return randomId.Next(100000, 999999);
    }
    public override List<Colaborador> ImportarColaboradores(Stream fileStream)
    {
        throw new NotImplementedException();
        // var colaboraciones = LeerCsv(fileStream);
        // var colaboracionesValidas = colaboraciones.Where(c =>
        //     _validador.Validar(c.TipoDoc, c.Documento, c.Nombre, c.Apellido, c.Mail, c.FechaColaboracion,
        //         c.FormaColaboracion, c.Cantidad)).ToList();
        //
        // var colaboradores = new List<Colaborador>();
        // var colaboracionesPorColaborador = colaboracionesValidas.GroupBy(c => new { c.TipoDoc, c.Documento });
        //
        // foreach (var colaborador in colaboracionesPorColaborador)
        // {
        //     var c = colaborador.ToList().Select(datos => Parsear(datos)).ToList();
        //     var col = c.FirstOrDefault();
        //     if (col == null) continue;
        //     c.RemoveAt(0);
        //     col.AgregarPuntos(c.Sum(x => x.ObtenerPuntos()));
        //     /*col.Id = GenerarId();*/
        //     colaboradores.Add(col);
        // }
        //
        // return colaboradores;
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

        private static UsuarioSistema Parsear(DatosColaboracion datos)
        {
            throw new NotImplementedException();
            /*var tipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), datos.TipoDoc);
            var documento = new DocumentoIdentidad(tipoDoc, datos.Documento, null);
            var colaborador = new PersonaHumana(GenerarId(), datos.Nombre, datos.Apellido, documento, null, null, null);
            var usuario = new UsuarioSistema(GenerarId(), colaborador, datos.Mail, CrearPassword());
            var tipoContribucion = (TipoContribucion)Enum.Parse(typeof(TipoContribucion), datos.FormaColaboracion);
            var contribucion = new List<FormaContribucion>();

            switch (tipoContribucion)
            {
                case TipoContribucion.DINERO:
                {
                    contribucion.Add(new DonacionMonetaria(
                        DateTime.Parse(datos.FechaColaboracion),
                        datos.Cantidad, 0));
                    break;
                }
                case TipoContribucion.DONACION_VIANDAS:
                {
                    for (var i = 0; i < datos.Cantidad; i++)
                    {
                        contribucion.Add(new DonacionVianda(DateTime.Parse(datos.FechaColaboracion), null, null));
                    }

                    break;
                }
                case TipoContribucion.REDISTRIBUCION_VIANDAS:
                {
                    contribucion.Add(new DistribucionViandas(DateTime.Parse(datos.FechaColaboracion), null, null,
                        datos.Cantidad, MotivoDistribucion.Desperfecto));
                    break;
                }
                case TipoContribucion.ENTREGA_TARJETAS:
                {
                    for (var i = 0; i < datos.Cantidad; i++)
                    {
                        contribucion.Add(new RegistroPersonaVulnerable(DateTime.Parse(datos.FechaColaboracion), null));
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

        return usuario;*/
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