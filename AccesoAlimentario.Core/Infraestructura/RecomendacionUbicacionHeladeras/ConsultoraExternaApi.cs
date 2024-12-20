using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Infraestructura.RecomendacionUbicacionHeladeras.Models;
using Newtonsoft.Json;
using RestSharp;

namespace AccesoAlimentario.Core.Infraestructura.RecomendacionUbicacionHeladeras;

public class ConsultoraExternaApi
{
    private const string Url = "https://f8286f90193a04f0926530d01c4def4d.m.pipedream.net/";

    public async Task<List<PuntoEstrategico>> GetRecomendacion(float latitud, float longitud, float radio)
    {
        var client = new RestClient(Url);
        var request = new RestRequest($"longitud={longitud}&latitud={latitud}&radio={radio}", Method.Get);
        var response = await client.GetAsync(request); //meterle un try catch
        var recomendacionesUbicacionResponse =
            JsonConvert.DeserializeObject<RecomendacionesUbicacionResponse>(response.Content ?? throw new InvalidOperationException());
        return recomendacionesUbicacionResponse?.Data.Select((d, i) => new PuntoEstrategico(
            $"opcion {i + 1}",
            float.Parse(d.Longitud),
            float.Parse(d.Latitud),
            new Direccion(
                d.Direccion.Calle,
                d.Direccion.Numero,
                d.Direccion.Localidad,
                d.Direccion.CodigoPostal)
        )).ToList() ?? throw new InvalidOperationException();
    }
}