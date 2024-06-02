using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Scoring;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Colaboradores;

public abstract class Colaborador
{
    private Usuario _usuario;
    private List<TipoContribucion> _tiposDeContribucionesElegidas; // solo para front
    public List<FormaContribucion> _contribucionesRealizadas;
    private List<IMedioContacto> _mediosDeContacto;
    private float _puntos;
    private DocumentoIdentidad _documentosIdentidad;

    public void Colaborar(FormaContribucion formaContribucion)
    {
        formaContribucion.AsignarColaborador(this);
        _contribucionesRealizadas.Add(formaContribucion);
        var score = new Score();
        score.Calcular(this, formaContribucion);
    }

    public abstract void Contactar();

    public float ObtenerPuntos()
    {
        return _contribucionesRealizadas.Sum(contribucion => contribucion.CalcularPuntos());
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