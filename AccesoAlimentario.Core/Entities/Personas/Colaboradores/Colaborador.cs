using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Direcciones;
using AccesoAlimentario.Core.Entities.MediosContacto;
using AccesoAlimentario.Core.Entities.Personas.DocumentosIdentidad;
using AccesoAlimentario.Core.Entities.Scoring;
using AccesoAlimentario.Core.Entities.Usuarios;

namespace AccesoAlimentario.Core.Entities.Personas.Colaboradores;

public abstract class Colaborador : Persona
{
    protected Usuario _usuario;
    protected List<TipoContribucion> _tiposDeContribucionesElegidas;
    protected List<FormaContribucion> _contribucionesRealizadas;
    protected List<MedioContacto> _mediosDeContacto;
    protected float _puntos;
    
    public Colaborador()
    {
    }
    public Colaborador(int id, string nombre, Direccion? direccion, DocumentoIdentidad? documentoIdentidad, Usuario usuario,
        List<TipoContribucion> tiposDeContribucionesElegidas)
        : base(id,nombre, direccion, documentoIdentidad)
    {
        Id = id;
        _usuario = usuario;
        _tiposDeContribucionesElegidas = tiposDeContribucionesElegidas;
        _contribucionesRealizadas = new List<FormaContribucion>();
        _mediosDeContacto = new List<MedioContacto>();
        _puntos = 0;
    }

    public void Colaborar(FormaContribucion formaContribucion)
    {
        if (formaContribucion.EsValido(this))
        {
            _contribucionesRealizadas.Add(formaContribucion);
            var score = new Score();
            score.Calcular(this, formaContribucion);
        }
    }

    public void Contactar(Notificacion notificacion)
    {
        _mediosDeContacto.FirstOrDefault()?.Enviar(notificacion);
    }

    public float ObtenerPuntos() => _puntos;

    public void DescontarPuntos(float valor)
    {
        _puntos -= valor;
    }

    public void AgregarPuntos(float valor)
    {
        _puntos += valor;
    }
}