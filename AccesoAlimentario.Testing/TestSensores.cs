using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Incidentes;
using AccesoAlimentario.Core.Entities.Sensores;

namespace AccesoAlimentario.Testing;

// Tests
[TestFixture]
public class SensorTemperaturaTests
{
    [Test]
    public void SensorTemperatura_RegistraIncidenciaTemperatura_True()
    {
        // Arrange
        var sensorTemperatura = new SensorTemperatura();
        var heladera = new Heladera { TemperaturaMinimaConfig = 2, TemperaturaMaximaConfig = 8 };
        heladera.AgregarSensor(sensorTemperatura);
        
        // Act
        sensorTemperatura.Registrar(DateTime.Now, "10");
    
        var heladeraIncidente = heladera.Incidentes[0];

        if (heladeraIncidente is Alerta alerta)
        {
            // Assert
            Assert.That(alerta.Tipo, Is.EqualTo(TipoAlerta.Temperatura), "La heladera registró la incidencia.");
        }
        else
        {
            Assert.Fail("El incidente no es de tipo alerta.");
        }
    }

    [Test]
    public void SensorTemperatura_RegistraIncidenciaTemperatura_False()
    {
        // Arrange
        var sensorTemperatura = new SensorTemperatura();
        var heladera = new Heladera { TemperaturaMinimaConfig = 2, TemperaturaMaximaConfig = 8 };
        heladera.AgregarSensor(sensorTemperatura);
        
        // Act
        sensorTemperatura.Registrar(DateTime.Now, "6");
        
        // Assert
        Assert.That(heladera.Incidentes, Has.Count.EqualTo(0), "La heladera no registró la incidencia.");
    }

    [Test]
    public void SensorTemperatura_ActualizaTemperaturaCorrectamente_True()
    {
        // Arrange
        var heladera = new Heladera();
        var sensorTemperatura = new SensorTemperatura();
        heladera.AgregarSensor(sensorTemperatura);

        // Act
        sensorTemperatura.Registrar(DateTime.Now, "6");

        // Assert
        Assert.That(heladera.TemperaturaActual, Is.EqualTo(6), "La heladera actualizó la temperatura correctamente.");
    }
    
    [Test]
    public void SensorTemperatura_ActualizaTemperaturaCorrectamente_False()
    {
        // Arrange
        var heladera = new Heladera();
        var sensorTemperatura = new SensorTemperatura();
        heladera.AgregarSensor(sensorTemperatura);

        // Act
        sensorTemperatura.Registrar(DateTime.Now, "A");

        // Assert
        Assert.That(heladera.TemperaturaActual, Is.EqualTo(0), "La heladera no actualizó la temperatura correctamente.");
    }
}

[TestFixture]
public class SensorMovimientoTests
{
    [Test]
    public void SensorMovimiento_DetectaMovimiento_True()
    {
        // Arrange
        var sensorMovimiento = new SensorMovimiento();
        var heladera = new Heladera();
        heladera.AgregarSensor(sensorMovimiento);

        // Act
        sensorMovimiento.Registrar(DateTime.Now, "true");

        var heladeraIncidente = heladera.Incidentes[0];
        
        if (heladeraIncidente is Alerta alerta)
        {
            // Assert
            Assert.That(alerta.Tipo, Is.EqualTo(TipoAlerta.Fraude), "La heladera generó una alerta de fraude.");
        }
        else
        {
            Assert.Fail("El incidente no es de tipo alerta.");
        }
    }
    
    [Test]
    public void SensorMovimiento_DetectaMovimiento_False()
    {
        // Arrange
        var sensorMovimiento = new SensorMovimiento();
        var heladera = new Heladera();
        heladera.AgregarSensor(sensorMovimiento);

        // Act
        sensorMovimiento.Registrar(DateTime.Now, "false");

        // Assert
        Assert.That(heladera.Incidentes, Has.Count.EqualTo(0), "La heladera no generó una alerta de fraude.");
    }
}