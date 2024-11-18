using AccesoAlimentario.Core.Entities.Premios;

namespace AccesoAlimentario.Core.Entities.Contribuciones;

public class OfertaPremio : FormaContribucion
{
    public virtual Premio Premio { get; set; } = null!;

    public OfertaPremio()
    {
    }
    
    public OfertaPremio(DateTime fechaContribucion, Premio premio)
        : base(fechaContribucion)
    {
        Premio = premio;
    }
}