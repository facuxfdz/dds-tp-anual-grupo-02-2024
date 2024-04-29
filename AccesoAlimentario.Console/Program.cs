// See https://aka.ms/new-console-template for more information

using AccesoAlimentario.Core.Entities;
using AccesoAlimentario.Core.Entities.Validadores.Passwords;

Console.WriteLine("Hello, World!");

PoliticaComplejidad holu = new PoliticaComplejidad();

Console.WriteLine(holu.Valida("hola"));

Console.WriteLine(holu.Valida("𐀀puto"));
