using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Premios;

public class Premio
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;
    public float PuntosNecesarios { get; set; } = 0;
    public string Imagen { get; set; } = null!;
    public virtual Colaborador? ReclamadoPor { get; set; } = null!;
    public DateTime FechaReclamo { get; set; } = DateTime.UtcNow;
    public TipoRubro Rubro { get; set; } = TipoRubro.Otros;

    public Premio()
    {
    }

    public Premio(string nombre, float puntosNecesarios, string imagen, TipoRubro rubro)
    {
        Nombre = nombre;
        PuntosNecesarios = puntosNecesarios;
        Imagen = imagen;
        Rubro = rubro;
    }

    public void Reclamar(Colaborador colaborador)
    {
        ReclamadoPor = colaborador;
        FechaReclamo = DateTime.UtcNow;
    }

    public float GetPuntosNecesarios()
    {
        return PuntosNecesarios;
    }
}