// using AccesoAlimentario.Core.Validadores.Usuarios;
//
// namespace AccesoAlimentario.Testing;
//
// public class TestPoliticasPassword
// {
//     private IPoliticaValidacion _politicaLongitud;
//     private IPoliticaValidacion _politica10KMasComunes;
//     private IPoliticaValidacion _politicaComplejidad;
//     private List<IPoliticaValidacion> _validaciones;
//
//     [SetUp]
//     public void Setup()
//     {
//         _politicaLongitud = new PoliticaLongitud();
//         _politica10KMasComunes = new Politica10KMasComunes();
//         _politicaComplejidad = new PoliticaComplejidad();
//         _validaciones =
//         [
//             _politicaLongitud,
//             _politica10KMasComunes,
//             _politicaComplejidad
//         ];
//     }
//
//     [Test]
//     public void PoliticaLongitud_MayorIgual8_True()
//     {
//         Assert.IsTrue(_politicaLongitud.Validar("12345678"));
//     }
//
//     [Test]
//     public void PoliticaLongitud_Menor8_False()
//     {
//         Assert.IsFalse(_politicaLongitud.Validar("1234567"));
//     }
//
//     [Test]
//     public void Politica10KMasComunes_ContraComun_False()
//     {
//         Assert.IsFalse(_politica10KMasComunes.Validar("123456"));
//     }
//
//     [Test]
//     public void Politica10KMasComunes_ContraNoComun_True()
//     {
//         Assert.IsTrue(_politica10KMasComunes.Validar("Pepito!Nicolas@Velez?Luchy#ProjectOwner"));
//     }
//
//     [Test]
//     public void PoliticaComplejidad_CaracteresMayorA9835Unicode_False()
//     {
//         Assert.IsFalse(_politicaComplejidad.Validar("𢯽"));
//     }
//
//     [Test]
//     public void PoliticaComplejidad_CaracteresUnicodeYAscii_True()
//     {
//         Assert.IsTrue(_politicaComplejidad.Validar("123a jaja-!~"));
//     }
//
//     [Test]
//     public void ValidadorContrasenias_123456_False()
//     {
//         var validador = new ValidadorContrasenias(_validaciones);
//         Assert.IsFalse(validador.Validar("123456"));
//     }
//
//     [Test]
//     public void ValidadorContrasenias_Aleatoria_True()
//     {
//         var validador = new ValidadorContrasenias(_validaciones);
//         Assert.IsTrue(validador.Validar("@Velez?Luchy#Projec"));
//     }
//
//     [Test]
//     public void ValidadorContrasenias_Vacio_False()
//     {
//         var validador = new ValidadorContrasenias(_validaciones);
//         Assert.IsFalse(validador.Validar(""));
//     }
// }