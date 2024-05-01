namespace AccesoAlimentario.Core.Settings;

public sealed class AppSettings
{
    public static readonly AppSettings Instance = Instance ?? new AppSettings();
    private string _pathArchivo = "Resources/10mil-mas-comunes.txt";
    private List<string> _contrasenias;
    
    private AppSettings()
    {
        try
        {
            _contrasenias = File.ReadAllLines(_pathArchivo).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public List<string> Contrasenias => _contrasenias;
}