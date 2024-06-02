using System.Globalization;
using AccesoAlimentario.Core.Interfaces.Validadores;
using CsvHelper;
using CsvHelper.Configuration;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class ImportacionCSV : FormaImportacion
{
    public override List<DatosColaboracion> leerArchivo(String file)
    {
        var colaboraciones = new List<DatosColaboracion>();
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
        };
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, configuration))
        {
            var records = csv.GetRecords<DatosColaboracion>();
            colaboraciones = records.ToList();
        }
        
        return colaboraciones;
        /* {
        Documento = new DocumentoIdentidad(
            Enum.Parse<TipoDocumento>(tipoDoc, true),
            int.Parse(documento),
            SexoDocumento.Otro
        );

        Nombre = nombre;
        Apellido = apellido;
        Mail = mail;
        FechaColaboracion = fechaColaboracion;
        FormaColaboracion = formaColaboracion;
        Cantidad = cantidad;

    }*/
    }
}