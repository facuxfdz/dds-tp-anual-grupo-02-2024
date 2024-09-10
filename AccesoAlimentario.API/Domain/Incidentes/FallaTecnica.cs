using AccesoAlimentario.API.Domain.Colaboraciones;
using AccesoAlimentario.API.Domain.Heladeras;

namespace AccesoAlimentario.API.Domain.Incidentes;

public class FallaTecnica : Incidente
{
    public Colaborador Colaborador = null!;
    public string Descripcion = string.Empty;
    public string Foto = string.Empty;

    public FallaTecnica(DateTime fecha, Heladera heladera, Colaborador colaborador, string descripcion, string foto) :
        base(fecha, heladera)
    {
        Colaborador = colaborador;
        Descripcion = descripcion;
        Foto = foto;
    }
}