// See https://aka.ms/new-console-template for more information

using AccesoAlimentario.Core.Entities;
using AccesoAlimentario.Core.Entities.CSV;
using AccesoAlimentario.Core.Entities.Validadores.Passwords;

Politica10KMasComunes politica10KMasComunes = new Politica10KMasComunes();

//Console.WriteLine(politica10KMasComunes.Valida("admin123"));

LecturaCsv read = new LecturaCsv();

List<Colaboracion> colaboraciones = read.LecturaCsvFile();

foreach (var colaboracion in colaboraciones)
{
    Console.WriteLine($"{colaboracion.TipoDoc},{colaboracion.Documento},{colaboracion.Nombre}" +
                      $"{colaboracion.Apellido},{colaboracion.Mail},{colaboracion.FechaColaboracion}," +
                      $"{colaboracion.FormaColaboracion},{colaboracion.Cantidad}");
}