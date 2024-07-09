// See https://aka.ms/new-console-template for more information

using AccesoAlimentario.Core.Validadores.Usuarios;
using AccesoAlimentario.Core.Email;


EmailService emailService = new EmailService();

await emailService.SendAsync("tuvieja@example.com", "nicoputo@example.com", "Tenemos a tu familia cautiva", "<h1>Hello friend!</h1>");

//Console.WriteLine(politica10KMasComunes.Valida("admin123"));
/*
LecturaCsv read = new LecturaCsv();

List<Colaboracion> colaboraciones = read.LecturaCsvFile();

foreach (var colaboracion in colaboraciones)
{
    Console.WriteLine($"{colaboracion.TipoDoc},{colaboracion.Documento},{colaboracion.Nombre}" +
                      $"{colaboracion.Apellido},{colaboracion.Mail},{colaboracion.FechaColaboracion}," +
                      $"{colaboracion.FormaColaboracion},{colaboracion.Cantidad}");
}
*/
//
