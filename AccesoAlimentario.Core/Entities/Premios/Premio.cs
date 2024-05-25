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

    public void Reclamar()
    {
        throw new NotImplementedException();
    }
}