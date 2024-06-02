using AccesoAlimentario.Core.Entities.Personas.Colaboradores;

namespace AccesoAlimentario.Core.Entities.Premios;

public class Premio
{
    private string _nombre;
    private float _puntosNecesarios;
    private string _imagen;
    private bool _reclamado;
    private TipoRubro _tipoRubro;
    
    public Premio(string nombre, float puntosNecesarios, string imagen, TipoRubro tipoRubro)
    {
        _nombre = nombre;
        _puntosNecesarios = puntosNecesarios;
        _imagen = imagen;
        _reclamado = false;
        _tipoRubro = tipoRubro;
    }

    public void Reclamar(Colaborador reclamante)
    {
        if (_reclamado)
        {
            throw new Exception("Este premio ya fue reclamado");
        }
        if(reclamante.ObtenerPuntos() >= _puntosNecesarios)
        {
            _reclamado = true;
            reclamante.DescontarPuntos(_puntosNecesarios);
        }
        else
        {
            throw new Exception("No tienes suficientes puntos para reclamar este premio");
        }
    }
}