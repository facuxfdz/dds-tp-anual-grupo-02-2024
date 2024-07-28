// using AccesoAlimentario.Core.DAL;
// using AccesoAlimentario.Core.Entities.Reportes;
//
// namespace AccesoAlimentario.Core.Servicios;
//
// public class ReportesServicio(UnitOfWork unitOfWork)
// {
//     public List<Reporte> ObtenerReportes()
//     {
//         var heladeras = unitOfWork.HeladeraRepository.Get().ToList();
//         var incidentes = unitOfWork.IncidenteRepository.Get().ToList();
//         var accesos = unitOfWork.AccesoHeladeraRepository.Get().ToList();
//         var colaboradores = unitOfWork.ColaboradorRepository.Get().ToList();
//         var reportador = new Reportador(heladeras, incidentes, accesos, colaboradores);
//         var thisWeekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
//         var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
//         return reportador.GenerarReportes(thisWeekStart, thisWeekEnd);
//     }
// }