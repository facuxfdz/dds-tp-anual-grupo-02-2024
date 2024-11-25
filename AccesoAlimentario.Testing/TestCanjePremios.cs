// using AccesoAlimentario.Core.DAL;
// using AccesoAlimentario.Core.Entities.Personas;
// using AccesoAlimentario.Core.Entities.Premios;
// using AccesoAlimentario.Core.Entities.Roles;
// using Microsoft.EntityFrameworkCore;
//
// namespace AccesoAlimentario.Testing;
//
// public class TestCanjePremios
// {
//     private Colaborador unColaboradorSinPuntos;
//     private Colaborador unColaboradorConPuntos;
//     private Premio unPremio;
//     private PremiosServicio premiosServicio;
//     private PersonaHumana unaPersonaHumana;
//
//     
//     [SetUp]
//     public void Setup()
//     {
//         unaPersonaHumana = new PersonaHumana("Pepe", "Gonzalez", null, null, null, SexoDocumento.Femenino);
//         unColaboradorSinPuntos = new Colaborador(unaPersonaHumana,null);
//         unColaboradorConPuntos = new Colaborador(unaPersonaHumana, null);
//         unColaboradorConPuntos.AgregarPuntos(250);
//
//         unPremio = new Premio("jamon", 200, null, TipoRubro.Gastronomia);
//         
//         var options = new DbContextOptionsBuilder<AppDbContext>()
//             .Options;
//         var dbcontext = new AppDbContext(options);
//         var unitOfWork = new UnitOfWork(dbcontext);
//         premiosServicio = new PremiosServicio(unitOfWork);
//     }
//  
//     // Cuando los puntos no son suficientes para canjear un premio...
//     // Testear que el canjeo de los puntos por premios no funciona cuando los puntos son insuficientes.
//     [Test]
//     public void canjearPremio_PuntosInSuficientes_NoPuedeCanjear()
//     {
//         Assert.Throws<InvalidOperationException>(() => premiosServicio.CanjearPremio(unPremio, unColaboradorSinPuntos));
//     }
//     
//     // Testear que al intentar canjear puntos insuficientes por un premio, la relacion premio-colaborador no es generada.
//     [Test]
//     public void canjearPremio_PuntosInsuficientes_NoGeneraRelacionPremioColaborador()
//     {
//         try
//         {
//             premiosServicio.CanjearPremio(unPremio, unColaboradorSinPuntos);
//         }
//         catch (InvalidOperationException)
//         {
//             Assert.IsNull(unPremio.ReclamadoPor);
//         }
//     }
//     
//     // los puntos no se restan de los disponibles en el colaborador
//     [Test]
//     public void canjearPremio_PuntosInsuficientes_NoDescuentaPuntosPremio()
//     {
//         try
//         {
//             premiosServicio.CanjearPremio(unPremio, unColaboradorSinPuntos);
//         }
//         catch (InvalidOperationException)
//         {
//             Assert.AreEqual(0, unColaboradorSinPuntos.Puntos);
//         }
//     }
//     
//     // Cuando los puntos son suficientes para canjear un premio...
//     // Testear que el canje de los puntos por premios funciona cuando los puntos son suficientes.
//     [Test]
//     public void canjearPremio_PuntosSuficientes_PuedeCanjear()
//     {
//         Assert.DoesNotThrow(() => premiosServicio.CanjearPremio(unPremio, unColaboradorConPuntos));    
//     }
//     
//     // genera la relacion premio-colaborador.
//     [Test]
//     public void canjearPremio_PuntosSuficientes_GeneraRelacionPremioColaborador()
//     {
//         premiosServicio.CanjearPremio(unPremio, unColaboradorConPuntos);
//         Assert.AreEqual(unColaboradorConPuntos, unPremio.ReclamadoPor);
//     }
//
//     // los puntos se restan de los disponibles en el colaborador
//     [Test]
//     public void canjearPremio_PuntosSuficientes_DescuentaPuntosPremio()
//     {
//         premiosServicio.CanjearPremio(unPremio, unColaboradorConPuntos);
//         Assert.AreEqual(50, unColaboradorConPuntos.Puntos);
//     }
//  }