using Microsoft.Extensions.Configuration;

namespace AccesoAlimentario.Core.Settings;

public sealed class AppSettings
{
    public static readonly AppSettings Instance = Instance ?? new AppSettings();
    private string _pathPasswordMasComunes = "Resources/10mil-mas-comunes.txt";
    private List<string> _contrasenias;
    private float _pesoDonadosCoef;
    private float _viandasDistribuidasCoef;
    private float _viandasDonadasCoef;
    private float _tarjetasRepartidasCoef;

    private AppSettings()
    {
        try
        {
            _contrasenias = File.ReadAllLines(_pathPasswordMasComunes).ToList();
            var build = new ConfigurationBuilder()
                .AddJsonFile("Resources\\CoeficientesPuntos.json", optional: true, reloadOnChange: true).Build();
            _pesoDonadosCoef = float.Parse(build["PesoDonadosCoef"]);
            _viandasDistribuidasCoef = float.Parse(build["ViandasDistribuidasCoef"]);
            _viandasDonadasCoef = float.Parse(build["ViandasDonadasCoef"]);
            _tarjetasRepartidasCoef = float.Parse(build["TarjetasRepartidasCoef"]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<string> Contrasenias => _contrasenias;

    public float PesoDonadosCoef => _pesoDonadosCoef;
    public float ViandasDistribuidasCoef => _viandasDistribuidasCoef;
    public float ViandasDonadasCoef => _viandasDonadasCoef;
    public float TarjetasRepartidasCoef => _tarjetasRepartidasCoef;
}