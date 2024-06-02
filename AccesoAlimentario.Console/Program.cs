// See https://aka.ms/new-console-template for more information

using AccesoAlimentario.Core.Entities.Colaboradores;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Validadores.Passwords;

Politica10KMasComunes politica10KMasComunes = new Politica10KMasComunes();

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
DateOnly fechaNacimiento = new DateOnly(1990, 1, 1);
PersonaHumana unColaborador = new PersonaHumana("pepita","pepa",fechaNacimiento,null);
DistribucionVianda formaContribucion1 = new DistribucionVianda(null, null, DateTime.Now, null, null, 10, MotivoDistribucion.Desperfecto);
unColaborador.Colaborar(formaContribucion1);