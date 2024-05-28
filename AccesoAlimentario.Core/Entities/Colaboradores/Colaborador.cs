using System.Diagnostics.CodeAnalysis;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Scoring;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public abstract class Colaborador
{
    private Usuario _usuario;
    public List<FormaContribucion> _formasDeContribucion;
    private List<IMedioContacto> _mediosDeContacto;
    private float _puntos;

    public virtual void Colaborar(FormaContribucion formaContribucion)
    {    formaContribucion.AsignarColaborador(this); 
        _formasDeContribucion.Add(formaContribucion);
        formaContribucion.Colaborar();
        Score score = new Score();
        score.Calcular(this, formaContribucion);
    }

    public abstract void Contactar();

    public float ObtenerPuntos()
    {
        return _formasDeContribucion.Sum(contribucion => contribucion.CalcularPuntos());
    }

    public void DescontarPuntos(float valor)
    {
        _puntos -= valor;
    }
    public void AgregarPuntos(float valor)
    {
        _puntos += valor;
    }
}