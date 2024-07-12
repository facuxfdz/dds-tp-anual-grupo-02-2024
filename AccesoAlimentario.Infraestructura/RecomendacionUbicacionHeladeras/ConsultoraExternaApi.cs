using AccesoAlimentario.Core.Entities.Heladeras;

namespace AccesoAlimentario.Infraestructura.RecomendacionUbicacionHeladeras;

public class ConsultoraExternaApi
{
    private const string Url = "https://d75bfa97eb02d0219b7537f2d6a286b7.m.pipedream.net/";

    public List<PuntoEstrategico> GetRecomendacion(string latitud, string longitud, float radio)
    {
        throw new NotImplementedException();
        /*var client = new RestClient(Url);
        var request = new RestRequest($"longitud={longitud}&latitud={latitud}&radio={radio}", Method.Get);
        var response = client.Get(request);
        var recomendacionesUbicacionResponse =
            JsonConvert.DeserializeObject<RecomendacionesUbicacionResponse>(response.Content);
        return recomendacionesUbicacionResponse?.data.Select((d, i) => new PuntoEstrategico(
            i + 1,
            $"opcion {i + 1}",
            float.Parse(d.Longitud),
            float.Parse(d.Latitud),
            
            new Direccion(
                0,    
                d.Direccion.Calle,
                d.Direccion.Numero,
                d.Direccion.Localidad,
                null,
                null,
                d.Direccion.CodigoPostal)
        )).ToList();*/
    }
}