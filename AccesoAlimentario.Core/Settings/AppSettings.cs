using AccesoAlimentario.Core.Infraestructura.EmailObj;
using Microsoft.Extensions.Configuration;

namespace AccesoAlimentario.Core.Settings;

public sealed class AppSettings
{
    public static readonly AppSettings Instance = Instance ?? new AppSettings();
    public string PathPasswordMasComunes = "Resources\\10mil-mas-comunes.txt";
    public List<string> Contrasenias { get; set; } = [];
    public float PesoDonadosCoef { get; set; }
    public float ViandasDistribuidasCoef { get; set; }
    public float ViandasDonadasCoef { get; set; }
    public float TarjetasRepartidasCoef { get; set; }
    public SmtpConfiguration SmtpConfig { get; set; }

    private AppSettings()
    {
        try
        {
            Contrasenias = File.ReadAllLines(PathPasswordMasComunes).ToList();
            var build = new ConfigurationBuilder()
                .AddJsonFile("Resources\\CoeficientesPuntos.json", optional: true, reloadOnChange: true)
                .AddJsonFile("Resources\\appsettings.json")
                .Build();
            PesoDonadosCoef = float.Parse(build["PesoDonadosCoef"]);
            ViandasDistribuidasCoef = float.Parse(build["ViandasDistribuidasCoef"]);
            ViandasDonadasCoef = float.Parse(build["ViandasDonadasCoef"]);
            TarjetasRepartidasCoef = float.Parse(build["TarjetasRepartidasCoef"]);
            SmtpConfig = new SmtpConfiguration
            {
                Host = build["Smtp:Host"],
                Port = int.Parse(build["Smtp:Port"]),
                Username = build["Smtp:Username"],
                Password = build["Smtp:Password"]
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}