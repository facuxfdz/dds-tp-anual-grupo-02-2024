// using System.Linq.Expressions;
// using AccesoAlimentario.Core.DAL;
// using AccesoAlimentario.Core.Entities.Heladeras;
// using AccesoAlimentario.Core.Entities.Incidentes;
// using AccesoAlimentario.Core.Entities.Reportes;
// using Microsoft.AspNetCore.Mvc;
//
// namespace AccesoAlimentario.API.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// public class ReportesController(UnitOfWork unitOfWork) : ControllerBase
// {
//     // POST: api/reportes
//     [HttpPost]
//     public IActionResult GenerarReportes()
//     {
//
//         var heladerasRepository = unitOfWork.HeladeraRepository;
//         var incidentesRepository = unitOfWork.IncidenteRepository;
//         var reportesRepository = unitOfWork.ReporteRepository;
//         var fallasHeladeraReporteSchemaRepository = unitOfWork.FallasHeladeraReporteSchemaRepository;
//         var buildersReporte = new List<IReporteBuilder>
//         {
//             new BuildFallasHeladeraReporte(reportesRepository,heladerasRepository, incidentesRepository,fallasHeladeraReporteSchemaRepository)
//             // new BuildAccionesViandasReporte(),
//             // new BuildViandasPorColaboradorReporte()
//         };
//         
//         var reportes = new List<Reporte>();
//         foreach (var builder in buildersReporte)
//         {
//             try
//             {
//                 var reporteGenerado = builder.Build();
//                 reportes.Add(reporteGenerado);
//                 Console.WriteLine($"Reporte generado con id: {reporteGenerado.Id}");
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//                 var errorResponse = new { error = "El proceso de generacion de reportes fallo" };
//                 return StatusCode(500, errorResponse);
//             }
//         }
//
//         foreach (var reporte in reportes)
//         {
//             Console.WriteLine($"Valido hasta: {reporte.ValidoHasta}");
//             Console.WriteLine($"Fecha generacion {reporte.FechaGeneracion}");
//         }
//         var responseOk = new { message = "Reportes generados correctamente", reportes = reportes };
//         return Ok(responseOk);
//     }
//     
//     [HttpGet]
//     public IActionResult ConsultarReportes()
//     {
//         // Consultar los datos del reporte de fallas tecnicas
//         // filtrando solo los datos cuyo reportes tenga una fecha de "validoHasta" mayor a la fecha actual
//         
//         Expression<Func<FallasHeladeraReporteSchema, bool>> filter = f => f.Reporte.ValidoHasta > DateTime.Now;
//         var datosFallasTecnicasReporte = unitOfWork.FallasHeladeraReporteSchemaRepository.Get(filter: filter, includeProperties: "Heladera,Reporte,Punto");
//         var responseOk = new { datosFallasTecnicasReporte = datosFallasTecnicasReporte };
//         return Ok(responseOk);
//     }
//     
//    
// }