using System;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using ApiRestConsultoraExterna.Models;
using RestSharp;
using Newtonsoft.Json;


namespace AccesoAlimentario.Core.ApiRestConsultoraExterna
{
    public class ConsultoraExternaApi
    {
        private readonly string _url = "https://d75bfa97eb02d0219b7537f2d6a286b7.m.pipedream.net/";

        public List<PuntoEstrategico> GetRecomendacion(string latitud, string longitud, float radio)
        {
            var client = new RestClient(_url);
            var request = new RestRequest($"longitud={longitud}&latitud={latitud}&radio={radio}", Method.Get);
            var response = client.Get(request);
            var recomendacionesUbicacionResponse = JsonConvert.DeserializeObject<RecomendacionesUbicacionResponse>(response.Content);
            return recomendacionesUbicacionResponse.data.Select((d,Index) => new PuntoEstrategico(
                $"opcion {Index + 1}",
                float.Parse(d.Longitud),
                float.Parse(d.Latitud),
                new Direccion(
                    d.Direccion.Calle,
                    d.Direccion.Numero,
                    d.Direccion.Localidad,
                    null,
                    null,
                    d.Direccion.CodigoPostal)
                )).ToList();
        }
    }
    
}