using System.Text.Json.Serialization;

namespace AccesoAlimentario.API.UseCases.RegistrarDataHeladera.DTO;

public class RegistroTemperaturaDto
{
    [JsonPropertyName("heladera")]
    public int Heladera { get; set; }
    
    [JsonPropertyName("temperatura")]
    public float Temperatura { get; set; }
    
    [JsonPropertyName("fechaLectura")]
    public DateTime FechaLectura { get; set; }
}