using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public abstract class Alerta : Incidente
{
    public Alerta(DateTime fecha, Heladera heladera) : base(fecha, heladera)
    {
    }
}