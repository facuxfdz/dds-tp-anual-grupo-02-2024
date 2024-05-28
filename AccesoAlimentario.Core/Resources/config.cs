using System;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

public static class Config
{
    public static float PesoDonadosCoef { get; private set; }
    public static float ViandasDistribuidasCoef { get; private set; }
    public static float ViandasDonadasCoef { get; private set; }
    public static float TarjetasRepartidasCoef { get; private set; }

    static Config()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("Resources\\CoeficientesPuntos.json", optional: true, reloadOnChange: true).Build();
        PesoDonadosCoef = float.Parse(config["PesoDonadosCoef"]);
        ViandasDistribuidasCoef = float.Parse(config["ViandasDistribuidasCoef"]);
        ViandasDonadasCoef = float.Parse(config["ViandasDonadasCoef"]);
        TarjetasRepartidasCoef = float.Parse(config["TarjetasRepartidasCoef"]);
    }
}