using AccesoAlimentario.Core.Entities.Colaboradores;

namespace AccesoAlimentario.Core.Entities.Premios;

public class Premio
{
    private string _nombre;
    private float _puntosNecesarios;
    private string _imagen;
    private bool _reclamado;
    
    public Premio(string nombre, float puntosNecesarios, string imagen)
    {
        _nombre = nombre;
        _puntosNecesarios = puntosNecesarios;
        _imagen = imagen;
        _reclamado = false;
    }

    public void Reclamar(Colaborador reclamante)
    {
        if(reclamante.obtenerPuntos() >= _puntosNecesarios)
        {
            _reclamado = true;
            reclamante.descontarPuntos(_puntosNecesarios);
        }
        else
        {
            throw new Exception("No tienes suficientes puntos para reclamar este premio");
        }
        
    }
}