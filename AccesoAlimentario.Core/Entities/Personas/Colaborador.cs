using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Scoring;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas;

public abstract class Colaborador : Persona
{
    protected Usuario _usuario;
    protected List<TipoContribucion> _tiposDeContribucionesElegidas; // solo para front
    protected List<FormaContribucion> _contribucionesRealizadas;
    protected List<IMedioContacto> _mediosDeContacto;
    protected float _puntos;

    public Colaborador(string nombre, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, Usuario usuario)
        : base(nombre, direccion, documentoIdentidad)
    {
        _usuario = usuario;
        _contribucionesRealizadas = new List<FormaContribucion>();
        _mediosDeContacto = new List<IMedioContacto>();
        _puntos = 0;
    }
    
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