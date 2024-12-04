using System.Globalization;
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

        public async Task ProcessMessageBuffered(List<string> messages)
        {
            try
            {
                List<AltaRegistroTemperatura.AltaRegistroTemperaturaCommand> records = new List<AltaRegistroTemperatura.AltaRegistroTemperaturaCommand>();

                // Parsear los mensajes y agregar los registros a la lista
                foreach (var message in messages)
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

                        records.Add(record);
                    }
                }

                // Agrupar los registros por SensorId y calcular el promedio de Temperatura por grupo
                var groupedBySensor = records
                    .GroupBy(r => r.SensorId)  // Agrupar por SensorId
                    .Select(group => new
                    {
                        SensorId = group.Key,
                        AverageTemperature = group.Average(r => double.Parse(r.Temperatura))
                    });

                // Procesar cada grupo (calcular el promedio y enviar)
                foreach (var group in groupedBySensor)
                {
                    Console.WriteLine($"SensorId: {group.SensorId}, Average Temperature: {group.AverageTemperature}");

                    // Enviar el registro agregado al sistema
                    await _mediator.Send(new AltaRegistroTemperatura.AltaRegistroTemperaturaCommand
                    {
                        SensorId = group.SensorId,  // Usar el SensorId del grupo
                        Fecha = DateTime.Now,
                        Temperatura = group.AverageTemperature.ToString(CultureInfo.InvariantCulture)
                    });
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        }
    }
}
