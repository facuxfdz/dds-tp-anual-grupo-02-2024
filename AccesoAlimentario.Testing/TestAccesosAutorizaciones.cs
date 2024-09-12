using AccesoAlimentario.Core.Entities.Autorizaciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Servicios;

namespace AccesoAlimentario.Testing;

[TestFixture]
public class TestAccesosAutorizaciones //Para que funcione la BD debe estar conectada, asi validamos que las tarjetas exsitan al menos y que las tarjetas de consumo
{
    private Heladera _heladera;
    private TarjetaColaboracion _tarjetaColaboracionValida;
    private TarjetaColaboracion _tarjetaColaboracionInvalida;

    [SetUp]
    public void Setup()
    {
        _heladera = new Heladera();
        _tarjetaColaboracionValida = new TarjetaColaboracion();
        _tarjetaColaboracionInvalida = new TarjetaColaboracion();
    }
    
    [Test]
    public void TestAccederHeladeraConTarjetaColabValida()
    {
        var autorizacion = new AccesoHeladera(_tarjetaColaboracionValida, _heladera, TipoAcceso.RetiroVianda);

        Assert.That(autorizacion.VerificarValidez(), Is.True, "La tarjeta válida debería permitir el acceso.");
    }
    
 /*   [Test]
    public void TestAccederHeladeraConTarjetaColabInValida()
    {
        var autorizacion = new AccesoHeladera(_tarjetaColaboracionInvalida, _heladera, TipoAcceso.RetiroVianda);

        Assert.That(autorizacion.VerificarValidez(), Is.False, "La tarjeta inválida no debería permitir el acceso.");
    }
    */
}
