using System.Globalization;
using AccesoAlimentario.Core.Entities.Personas.Colaboradores;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Usuarios;
using AccesoAlimentario.Core.Validadores.ImportacionMasiva;
using CsvHelper;
using CsvHelper.Configuration;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ImportadorCsv : FormaImportacion
{
    private readonly ValidadorImportacionMasiva _validador = new();

    public override List<Colaborador> ImportarColaboradores(string file)
    {
        var colaboraciones = LeerCsv(file);
        var colaboradores = new List<Colaborador>();
        foreach (var datos in colaboraciones)
        {
            if (_validador.Validar(datos.TipoDoc, datos.Documento, datos.Nombre, datos.Apellido, datos.Mail,
                    datos.FechaColaboracion, datos.FormaColaboracion, datos.Cantidad))
            {
                colaboradores.Add(Parsear(datos));
            }
        }
        return colaboradores;
    }

    private static List<DatosColaboracion> LeerCsv(string file)
    {
        List<DatosColaboracion> colaboraciones;
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
        };
        using var reader = new StreamReader(file);
        using var csv = new CsvReader(reader, configuration);
        var records = csv.GetRecords<DatosColaboracion>();
        colaboraciones = records.ToList();

        return colaboraciones;
    }

    private static Colaborador Parsear(DatosColaboracion datos)
    {
        var tipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), datos.TipoDoc);
        var documento = new DocumentoIdentidad(tipoDoc, datos.Documento, SexoDocumento.Otro);
        var usuario = new Usuario(datos.Mail, CrearPassword(), false);
        var colaborador = new PersonaHumana(datos.Nombre, datos.Apellido,null, null, documento, usuario, []);
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