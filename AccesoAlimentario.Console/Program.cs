// See https://aka.ms/new-console-template for more information

using AccesoAlimentario.Core.Entities;
using AccesoAlimentario.Core.Entities.Validadores.Passwords;


//Politica10KMasComunes politica10KMasComunes = new Politica10KMasComunes();

//Console.WriteLine(politica10KMasComunes.Valida("admin123"));

var apiRestConsultoraExterna = new AccesoAlimentario.Core.ApiRestConsultoraExterna.ApiRestConsultoraExterna();
var recomendaciones = apiRestConsultoraExterna.GetRecomendacion("123", "123", 123);

Console.WriteLine("aaaa");