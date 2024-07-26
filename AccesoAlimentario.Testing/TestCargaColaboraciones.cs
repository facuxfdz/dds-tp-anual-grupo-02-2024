using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Infraestructura.ImportacionColaboradores;
using AccesoAlimentario.Core.Validadores.ImportacionMasiva;

namespace AccesoAlimentario.Testing
{
    [TestFixture]
    public class TestCargaColaboraciones
    {
        private MockRepository<Colaborador> _mockColaboradorRepository;
        private ValidadorImportacionMasiva _validador;
        private ImportadorColaboraciones _importador;

        [SetUp]
        public void Setup()
        {
            _mockColaboradorRepository = new MockRepository<Colaborador>();
            _validador = new ValidadorImportacionMasiva();
            _importador = new ImportadorColaboraciones(_mockColaboradorRepository, _validador);
        }

        [Test]
        public void Importar_DatosInvalidosEnFilas_DeberiaLanzarExcepcion()
        {
            // Arrange
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("TipoDoc,Documento,Nombre,Apellido,Mail,FechaColaboracion,FormaColaboracion,Cantidad");
            writer.WriteLine("INVALIDO,12345678,John,Doe,john@example,01/01/2020,DINERO,100"); // TipoDoc inválido
            writer.Flush();
            stream.Position = 0;

            // Assert that an exception is thrown
            var ex = Assert.Throws<Exception>(() => _importador.Importar(stream));
            Assert.That(ex.Message, Is.EqualTo("Error al importar los colaboradores. El archivo contiene datos inválidos."));
        }

        [Test]
        public void Importar_ColumnasInvalidas_NoDebeIniciarCarga()
        {
            // Arrange
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("Documento,Nombre,Apellido,Mail,FechaColaboracion,FormaColaboracion,Cantidad"); // Falta TipoDoc
            writer.Flush();
            stream.Position = 0;

            // Assert that an exception is thrown
            var ex = Assert.Throws<Exception>(() => _importador.Importar(stream));
            Assert.That(ex.Message, Is.EqualTo("Error al importar los colaboradores. El archivo no pudo ser leído."));
        }

        [Test]
        public void Importar_DatosNulos_DeberiaLanzarExcepcion()
        {
            // Arrange
            Stream? stream = null;

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _importador.Importar(stream));
            Assert.That(ex.Message, Is.EqualTo("Error al importar los colaboradores. El archivo no pudo ser leído."));
            
        }
    }
}
