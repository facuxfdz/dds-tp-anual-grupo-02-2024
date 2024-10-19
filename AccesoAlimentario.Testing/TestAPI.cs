using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Moq; 

namespace AccesoAlimentario.Testing
{
    [TestFixture]
    public class TestApi
    {
        private JObject _respuesta;
        private string _latitud;
        private string _longitud;
        private Mock<HttpClient> _mockHttpClient;

        [SetUp]
        public void Setup()
        {
            // Podemos pegarle a la API? Si, es mas facil esto? Tambien
            string jsonResponse = @"{
                'Longitud': '-34.568990',
                'Latitud': '-58.491239',
                'Direccion': {
                    'Calle': 'Quesada',
                    'Numero': '5211',
                    'Localidad': 'Villa Urquiza',
                    'CodigoPostal': '1431'
                }
            }";

            _respuesta = JObject.Parse(jsonResponse);

            // Mock long y lat
            _latitud = "-58.491239";
            _longitud = "-34.568990";

            _mockHttpClient = new Mock<HttpClient>();
        }

        [Test]
        public void TestFormatoDeRespuesta()
        {
           
            Assert.That(_respuesta["Latitud"], Is.Not.Null, "Falta el campo 'Latitud'");
            Assert.That(_respuesta["Longitud"], Is.Not.Null, "Falta el campo 'Longitud'");
            Assert.That(_respuesta["Direccion"], Is.Not.Null, "Falta el campo 'Direccion'");

            
            var direccion = _respuesta["Direccion"];
            Assert.That(direccion["Calle"], Is.Not.Null, "Falta el campo 'Calle'");
            Assert.That(direccion["Numero"], Is.Not.Null, "Falta el campo 'Numero'");
            Assert.That(direccion["Localidad"], Is.Not.Null, "Falta el campo 'Localidad'");
            Assert.That(direccion["CodigoPostal"], Is.Not.Null, "Falta el campo 'CodigoPostal'");
        }

        [Test]
        public void TestFormatoLatitudLongitud()
        {
            // Expresión regular para validar latitud y longitud en grados decimales
            string latLonPattern = @"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$";
            
           
            Assert.That(Regex.IsMatch(_latitud, latLonPattern), Is.True, "Latitud en formato incorrecto");
            
            
            Assert.That(Regex.IsMatch(_longitud, latLonPattern), Is.True, "Longitud en formato incorrecto");
        }

    }
}
