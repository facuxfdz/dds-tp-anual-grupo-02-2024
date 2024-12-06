using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Reportes;
using AccesoAlimentario.Operations.Dto.Requests.Direcciones;
using AccesoAlimentario.Operations.Dto.Requests.DocumentosDeIdentidad;
using AccesoAlimentario.Operations.Dto.Requests.Heladeras;
using AccesoAlimentario.Operations.Dto.Requests.Personas;
using AccesoAlimentario.Operations.Dto.Requests.Tarjetas;
using AccesoAlimentario.Operations.Dto.Responses.Reportes;

namespace AccesoAlimentario.Testing.Utils;

public static class MockRequest
{
    public static DireccionRequest GetDireccionRequest()
    {
        
        var direccionRequest = new DireccionRequest
        {
            Calle = "Calle 123",
            Departamento = "A 23",
            CodigoPostal = "1234",
            Localidad = "La Matanza",
            Numero = "123",
            Piso = "3"
        };
        
        return direccionRequest;
        

    }

    public static TarjetaConsumoRequest GetTarjetaConsumoRequest()
    {
        var tarjetaConsumoRequest = new TarjetaConsumoRequest
        {
            Codigo = "12433fgsa",
            Tipo = "Tarjeta",

        };
        return tarjetaConsumoRequest;
    }

    public static PersonaHumanaRequest GetPersonaRequest()
    {
        var personaRequest = new PersonaHumanaRequest
        {
            Nombre = "Roberto",
            Apellido = "Carlos",
            Sexo = SexoDocumento.Masculino
        };
        return personaRequest;
    }

    public static DocumentoIdentidadRequest GetDocumentoIdentidadRequest()
    {
        var documentoIdentidadRequest = new DocumentoIdentidadRequest
        {
            FechaNacimiento = DateTime.UtcNow,
            NroDocumento = "45679303",
            TipoDocumento = TipoDocumento.DNI
        };
        return documentoIdentidadRequest;
    }

    public static PuntoEstrategicoRequest GetPuntoEstrategicoRequest()
    {
        var direccionRequest = GetDireccionRequest();
        var puntoEstrategicoRequest = new PuntoEstrategicoRequest
        {
            Nombre = "Medrano",
            Direccion = direccionRequest,
            Longitud = 350859,
            Latitud = 2785
        };
        return puntoEstrategicoRequest;
    }

    public static SensorRequest GetSensorTemperaturaRequest()
    {
        var sensorRequest = new SensorTemperaturaRequest
        {
            Tipo = "Temperatura",

        };
        return sensorRequest;
    }

    public static ModeloHeladeraRequest GetModeloHeladeraRequest()
    {
        var modeloHeladera = new ModeloHeladeraRequest
        {
            Capacidad = 29,
            TemperaturaMinima = -20,
            TemperaturaMaxima = 24,
        };
        return modeloHeladera;
    }

    public static ReporteResponse GetReporteResponse()
    {
        var reporteCantidadFallasPorHeladera = new ReporteResponse
        {
            Tipo = TipoReporte.CANTIDAD_FALLAS_POR_HELADERA,
            FechaExpiracion = DateTime.UtcNow.AddDays(-1),
            Cuerpo = "Reporte de prueba"
        };

        return reporteCantidadFallasPorHeladera;
    }
    
        
    
}