using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Settings;
using AccesoAlimentario.Core.Validadores.Contribuciones;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class DonacionVianda : FormaContribucion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    protected readonly ValidadorContribuciones _validadorContribuciones = new ValidadorDonacionVianda();
    private Heladera _heladera;
    private Vianda _vianda;

    public DonacionVianda()
    {
    }
    public DonacionVianda(DateTime fechaContribucion, Heladera heladera, Vianda vianda)
        : base(fechaContribucion)
    {
        _heladera = heladera;
        _vianda = vianda;
    }

    public override float CalcularPuntos()
    {
        return AppSettings.Instance.ViandasDonadasCoef;
    }
}