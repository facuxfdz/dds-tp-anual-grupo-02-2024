using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class RegistroTemperatura
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public DateTime Date { get; set; } = DateTime.Now;
    public float Temperatura { get; set; } = 0;

    public RegistroTemperatura()
    {
    }

    public RegistroTemperatura(DateTime date, float temperatura)
    {
        Date = date;
        Temperatura = temperatura;
    }
}