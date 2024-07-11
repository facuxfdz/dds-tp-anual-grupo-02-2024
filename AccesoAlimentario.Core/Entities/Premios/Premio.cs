using AccesoAlimentario.Core.Entities.Roles;

namespace AccesoAlimentario.Core.Entities.Premios;

public class Premio
{
    private string _nombre;
    private float _puntosNecesarios;
    private string _imagen;
    private Colaborador? _reclamadoPor;
    private DateTime _fechaReclamo;
    private TipoRubro _rubro;
    
    public Premio(string nombre, float puntosNecesarios, string imagen, TipoRubro rubro)
    {
        _nombre = nombre;
        _puntosNecesarios = puntosNecesarios;
        _imagen = imagen;
        _rubro = rubro;
    }

    public void Reclamar(Colaborador colaborador)
    {
        _reclamadoPor = colaborador;
        _fechaReclamo = DateTime.Now;
    }

    public float GetPuntosNecesarios()
    {
        return _puntosNecesarios;
    }
}