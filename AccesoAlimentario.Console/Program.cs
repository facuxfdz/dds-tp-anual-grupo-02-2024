// See https://aka.ms/new-console-template for more information

using AccesoAlimentario.Core.Entities;
using AccesoAlimentario.Core.Entities.Validadores.Passwords;

Console.WriteLine("Hello, World!");

Politica10KMasComunes politica10KMasComunes = new Politica10KMasComunes();

Console.WriteLine(politica10KMasComunes.Valida("password"));
