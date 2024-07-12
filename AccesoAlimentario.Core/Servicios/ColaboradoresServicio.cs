using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;

namespace AccesoAlimentario.Core.Servicios;

public class ColaboradoresServicio (UnitOfWork unitOfWork, PersonasServicio personasServicio) 
{
    public Colaborador Crear(Persona persona, List<TipoContribucion> tipoContribuciones)
    {
        //TODO validar que la persona este cargada en la base de datos
        Colaborador colab = new Colaborador(persona, tipoContribuciones);
        unitOfWork.Colaboradores.Insert(colab);
        personasServicio.AgregarRol(persona.Id, colab);

        return colab;
    }

    public void Eliminar(int id)
    {
        var colab = unitOfWork.ColaboradorRepository.GetById(id);
        if (colab != null)
            unitOfWork.ColaboradorRepository.Delete(colab);
        else
        {
            throw new Exception("No se encontro el colaborador");
        }
    }

    public void Modificar(Colaborador colaborador, List<TipoContribucion> contriPrefe)
    {
        //TODO falta la parte de persistir los cambios en la base de datos
        colaborador.ContribucionesPreferidas = contriPrefe;
    }
    public ICollection<Colaborador> ObtenerTodos() 
    {
        return unitOfWork.Colaboradores.Get();
    }
    public Colaborador Buscar(Persona persona)
    {
        return unitOfWork.Colaboradores.Get().Find(c => c.Persona == persona);
    }
    public void AsignarTarjeta(Colaborador colaborador, Tarjeta tarjeta) 
    {
        //TODO falta la parte de persistir los cambios en la base de datos
        //unitOfWork.Tarjetas.Insert(tarjeta);
        colaborador.TarjetaColaboracion = tarjeta;
    }

    public float ObtenerPuntos(Colaborador colaborador) 
    {
        return colaborador.Puntos;        
    }

    public void AgregarPuntos(Colaborador colaborador,float puntos) 
    {
        //TODO falta la parte de persistir los cambios en la base de datos
        colaborador.Puntos += puntos;        
    }

    public void DescontarPuntos(Colaborador colaborador,float puntos) 
    {
        //TODO falta la parte de persistir los cambios en la base de datos
        colaborador.Puntos -= puntos;        
    }

    public void AgregarSuscripcion(Colaborador colaborador, Heladera heladera, Suscripcion suscripcion)
    {
        //TODO
        throw new NotImplementedException();
    }

    public void EliminarSuscripcion(Colaborador colaborador, Heladera heladera, Suscripcion suscripcion)
    {
        //TODO
        throw new NotImplementedException();
    }

    public void DesuscribirseAHeladera(Suscripcion suscripcion)
    {        
        //TODO
        throw new NotImplementedException();
    }
}