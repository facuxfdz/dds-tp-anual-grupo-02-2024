namespace AccesoAlimentario.Core.Infraestructura.RecomendacionUbicacionHeladeras.Models;

public class DatosResponse
{
    public string Longitud { get; set; } = null!;
    public string Latitud { get; set; } = null!;
    public DireccionResponse Direccion { get; set; } = null!;
}

public class DireccionResponse
{
    public string Calle { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string Localidad { get; set; } = null!;
    public string CodigoPostal { get; set; } = null!;
}

public class RecomendacionesUbicacionResponse
{
    public List<DatosResponse> data { get; set; } = null!;
}

