namespace AccesoAlimentario.API.Domain.Colaboraciones.Suscripciones;

public class ViandasRestantes : EventoHeladera
{
    // limite de viandas restantes. A partir de este valor se envia una notificacion al colaborador
    public int threshold { get; private set; }
    public ViandasRestantes(){}
    public ViandasRestantes(int threshold, List<Colaborador> suscriptores) : base(suscriptores)
    {
        this.threshold = threshold;
    }
}