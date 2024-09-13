using System.Text.Json.Serialization;

namespace AccesoAlimentario.API.UseCases.RegistrarDataHeladera.DTO;

public class RegistroFraudeDto
{
    [JsonPropertyName("heladera")]
    public int Heladera { get; set; }
    
    [JsonPropertyName("fechaLectura")]
    public DateTime FechaLectura { get; set; }
}