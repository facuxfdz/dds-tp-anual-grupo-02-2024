using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class RegistroPersonaVulnerable : FormaContribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    protected readonly ValidadorContribuciones _validadorContribuciones = new ValidadorRegistroPersonaVulnerable();
    private Tarjeta _tarjeta;

    public RegistroPersonaVulnerable()
    {
    }
    public RegistroPersonaVulnerable(DateTime fechaContribucion, Tarjeta tarjeta)
        : base(fechaContribucion)
    {
        _tarjeta = tarjeta;
    }

    public override float CalcularPuntos()
    {
        return AppSettings.Instance.TarjetasRepartidasCoef;
    }
}