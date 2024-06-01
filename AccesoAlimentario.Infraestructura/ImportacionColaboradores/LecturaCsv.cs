using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace AccesoAlimentario.Infraestructura.ImportacionColaboradores;

public class LecturaCsv
{
    private string _pathArchivo = "Resources/csvE2.csv";
    
    public List<Colaboracion> LecturaCsvFile()
    {
        var colaboraciones = new List<Colaboracion>(); 
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
        };
        using (var reader = new StreamReader(_pathArchivo))
        using (var csv = new CsvReader(reader, configuration))
        {
            var records = csv.GetRecords<Colaboracion>();
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