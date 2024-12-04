using System.Text.Json;
using MediatR;
using AccesoAlimentario.Operations.Sensores.Temperatura;

namespace AccesoAlimentario.Web.SensoresReader.Processors
{
    public class TemperaturaProcessor
    {
        private readonly ISender _mediator;

        public TemperaturaProcessor(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task ProcessMessage(string message)
        {
            try
            {
                var normalizedMessage = message.Replace("'", "\"");
                
                using (var doc = JsonDocument.Parse(normalizedMessage))
                {
                    var root = doc.RootElement;

                    var record = new AltaRegistroTemperatura.AltaRegistroTemperaturaCommand
                    {
                        SensorId = Guid.Parse(root.GetProperty("SensorId").GetString()),
                        Fecha = root.GetProperty("Fecha").GetDateTime(),
                        Temperatura = root.GetProperty("Temperatura").GetString()
                    };

                    await _mediator.Send(record);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        }
    }
}