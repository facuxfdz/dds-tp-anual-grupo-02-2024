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
    public Heladera Heladera;
    public Vianda Vianda;

    public DonacionVianda()
    {
    }
    public DonacionVianda(DateTime fechaContribucion, Heladera heladera, Vianda vianda)
        : base(fechaContribucion)
    {
        Heladera = heladera;
        Vianda = vianda;
    }

}