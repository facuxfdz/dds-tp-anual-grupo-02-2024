// using AccesoAlimentario.API.Controllers.RequestDTO;
// using AccesoAlimentario.Core.Entities.Heladeras;
// using AccesoAlimentario.Core.Servicios;
// using Microsoft.AspNetCore.Mvc;
//
// namespace AccesoAlimentario.API.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// public class ModeloHeladeraController(ModeloHeladeraServicio servicio) : ControllerBase
// {
//     // POST: api/modeloHeladera
//     [HttpPost]
//     public IActionResult AddModeloHeladera(
//         [FromBody] ModeloHeladeraDTO modeloHeladera
//     )
//     {
//         ModeloHeladera modelo;
//         try
//         {
//             modelo = servicio.Crear(
//                 modeloHeladera.Id,
//                 modeloHeladera.Capacidad,
//                 modeloHeladera.TemperaturaMinima,
//                 modeloHeladera.TemperaturaMaxima
//             );
//         }
//         catch (Exception e)
//         {
//             return StatusCode(500, new { error = e.Message });
//         }
//         
//         return Ok(new { modeloId = modelo.Id });
//     }
//     
//     // GET: api/modeloHeladera
//     [HttpGet]
//     public IActionResult GetModeloHeladeras()
//     {
//         List<ModeloHeladera> modelos;
//         try
//         {
//             modelos = servicio.Listar();
//         }
//         catch (Exception e)
//         {
//             return StatusCode(500, new { error = e.Message });
//         }
//         
//         return Ok(new { modelos = modelos });
//     }
// }