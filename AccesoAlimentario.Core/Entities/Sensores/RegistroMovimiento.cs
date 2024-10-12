using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoAlimentario.Core.Entities.Sensores;

public class RegistroMovimiento
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Date { get; set; } = DateTime.Now;
    public bool Movimiento { get; set; } = false;

    public RegistroMovimiento()
    {
    }

    public RegistroMovimiento(DateTime date, bool movimiento)
    {
        Date = date;
        Movimiento = movimiento;
    }
}