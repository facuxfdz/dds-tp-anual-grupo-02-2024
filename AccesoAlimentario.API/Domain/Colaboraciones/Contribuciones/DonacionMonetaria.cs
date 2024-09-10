namespace AccesoAlimentario.API.Domain.Colaboraciones.Contribuciones;

public class DonacionMonetaria : Contribucion
{
    public float Monto { get; set; } = 0;
    public int FrecuenciaDias { get; set; } = 0;
    
    public DonacionMonetaria()
    {
    }

    public DonacionMonetaria(DateTime fechaContribucion, float monto, int frecuenciaDias)
        : base(fechaContribucion)
    {
        Monto = monto;
        FrecuenciaDias = frecuenciaDias;
    }
}