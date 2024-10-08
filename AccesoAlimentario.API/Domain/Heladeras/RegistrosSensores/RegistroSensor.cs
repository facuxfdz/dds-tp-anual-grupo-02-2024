using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;

public abstract class RegistroSensor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public Heladera Heladera { get; set; }
    public DateTime FechaLectura { get; set; }

    public RegistroSensor()
    {
    }
    
    public RegistroSensor(Heladera heladera, DateTime fechaLectura)
    {
        Heladera = heladera;
        FechaLectura = fechaLectura;
    }
    
}