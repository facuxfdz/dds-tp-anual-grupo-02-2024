// using AccesoAlimentario.Core.Entities.Autorizaciones;
// using AccesoAlimentario.Core.Entities.Contribuciones;
// using AccesoAlimentario.Core.Entities.Heladeras;
// using AccesoAlimentario.Core.Entities.Incidentes;
// using AccesoAlimentario.Core.Entities.Personas;
// using AccesoAlimentario.Core.Entities.Roles;
//
// namespace AccesoAlimentario.Core.Entities.Reportes;
//
// public class ReporteBuilderColaboradorViandasDonadas : IReporteBuilder
// {
//     public ReporteBuilderColaboradorViandasDonadas()
//     {
//     }
//
//     public Reporte Generar(DateTime fechaInicio, DateTime fechaFinal, List<Heladera> heladeras,
//         List<Incidente> incidentes, List<AccesoHeladera> accesos, List<Colaborador> colaboradores)
//     {
//         var descripcion = $"Reporte de viandas donadas por colaborador \n Periodo: {fechaInicio:ddMMyy} - {fechaFinal:ddMMyy}";
//         var cuerpo = "Detalle: \n";
//
//         foreach (var colaborador in colaboradores)
//         {
//             var donacionesRealizadasEnIntervaloValido = colaborador.ContribucionesRealizadas.Where(c =>
//                 c.FechaContribucion.Date > fechaInicio.Date && c.FechaContribucion.Date < fechaFinal.Date).ToList();
//             var viandasDonadas = donacionesRealizadasEnIntervaloValido.Count(v => v is DonacionVianda);
//             if (viandasDonadas > 0)
//             {
//                 switch (colaborador.Persona)
//                 {
//                     case PersonaHumana personaHumana:
//                         cuerpo += $"Colaborador: {personaHumana.Nombre} {personaHumana.Apellido} donó {viandasDonadas} viandas\n";
//                         break;
//                     case PersonaJuridica personaJuridica:
//                         cuerpo += $"Colaborador: {personaJuridica.Nombre} {personaJuridica.RazonSocial} donó {viandasDonadas} viandas\n";
//                         break;
//                 }
//             }
//             else
//             {
//                 switch (colaborador.Persona)
//                 {
//                     case PersonaHumana personaHumana:
//                         cuerpo += $"Colaborador: {personaHumana.Nombre} {personaHumana.Apellido} no donó ninguna vianda\n";
//                         break;
//                     case PersonaJuridica personaJuridica:
//                         cuerpo += $"Colaborador: {personaJuridica.Nombre} {personaJuridica.RazonSocial} no donó ninguna vianda\n";
//                         break;
//                 }
//             } 
//         }
//         return new Reporte(descripcion, cuerpo);
//     }
// }