using System;
using System.Configuration;

public static class Config
{
    public static float PesoDonadosCoef { get; private set; }
    public static float ViandasDistribuidasCoef { get; private set; }
    public static float ViandasDonadasCoef { get; private set; }
    public static float TarjetasRepartidasCoef { get; private set; }

    static Config()
    {
        PesoDonadosCoef = float.Parse(ConfigurationManager.AppSettings["PesoDonadosCoef"]);
        ViandasDistribuidasCoef = float.Parse(ConfigurationManager.AppSettings["ViandasDistribuidasCoef"]);
        ViandasDonadasCoef = float.Parse(ConfigurationManager.AppSettings["ViandasDonadasCoef"]);
        TarjetasRepartidasCoef = float.Parse(ConfigurationManager.AppSettings["TarjetasRepartidasCoef"]);
    }
}