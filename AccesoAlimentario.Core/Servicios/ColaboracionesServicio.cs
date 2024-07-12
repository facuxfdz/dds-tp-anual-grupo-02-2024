using AccesoAlimentario.Core.DAL;
using AccesoAlimentario.Core.Entities.Contribuciones;
using AccesoAlimentario.Core.Entities.Heladeras;
using AccesoAlimentario.Core.Entities.Personas;
using AccesoAlimentario.Core.Entities.Premios;
using AccesoAlimentario.Core.Entities.Roles;
using AccesoAlimentario.Core.Entities.Tarjetas;
using AccesoAlimentario.Core.Settings;

namespace AccesoAlimentario.Core.Servicios;

//TODO Los validadores de la forma de contribucion no deberian estar en la forma en si, ya que hay que primero crear el objeto al pedo y despues 
public class ColaboracionesServicio
{
    private UnitOfWork _unitOfWork;
    public ColaboracionesServicio(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void cargarColaboracionCsv(FormaContribucion formaContribucion, Colaborador colab)
    {
        switch (formaContribucion)
        {
            case DistribucionViandas distribucionViandas:
                var cantViandas = distribucionViandas.CantViandas;
                
                colab.AgregarPuntos(AppSettings.Instance.TarjetasRepartidasCoef * cantViandas);
                _unitOfWork.DistribucionViandasRepository.Insert(distribucionViandas);           
                break;
            case RegistroPersonaVulnerable registroPersonaVulnerable:
                colab.AgregarPuntos(AppSettings.Instance.TarjetasRepartidasCoef);
                _unitOfWork.RegistroPersonaVulnerableRepository.Insert(registroPersonaVulnerable);
                break;
            case DonacionMonetaria donacionMonetaria:
                var monto = donacionMonetaria.Monto;
                colab.AgregarPuntos(AppSettings.Instance.PesoDonadosCoef * monto);
                _unitOfWork.DonacionMonetariaRepository.Insert(donacionMonetaria);
                break;
            case DonacionVianda donacionVianda:
                colab.AgregarPuntos(AppSettings.Instance.ViandasDonadasCoef);
                _unitOfWork.DonacionViandaRepository.Insert(donacionVianda);
                break;
        }
    }
    public FormaContribucion crearAdministracionHeladera(Colaborador colab, Heladera heladera, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        AdministracionHeladera formaAdministracionHeladera = new AdministracionHeladera(fecha, heladera);
        if (!verificarColab(formaAdministracionHeladera, fechaContr, colab))
        {
           throw new Exception("No tiene autorizacion para realizar esta colaboracion" );
        }

        //Se le asignan los puntos al colaborador
        //Nada que agregar, ya que no se le asignan puntos por esta colaboracion

        return formaAdministracionHeladera;
    }

    public FormaContribucion crearDistribucionViandas(Colaborador colab,Heladera heladeraOrigen, Heladera heladeraDestino, int cant_viandas, MotivoDistribucion motivo, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;

        DistribucionViandas formaDistribucionViandas = new DistribucionViandas(fecha, heladeraOrigen, heladeraDestino, cant_viandas, motivo);
        
        if (!verificarColab(formaDistribucionViandas, fechaContr, colab))
        {
           throw new Exception("No tiene autorizacion para realizar esta colaboracion" );
        }

        //Se le asignan los puntos al colaborador
        colab.AgregarPuntos(AppSettings.Instance.TarjetasRepartidasCoef * cant_viandas); 

        return formaDistribucionViandas;
    }

    public FormaContribucion crearRegistroPersonaVulnerable(Colaborador colab, Persona persona, int cantMenores, TarjetaConsumo tarjetaConsumo, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        PersonaVulnerable personaVulnerable = new PersonaVulnerable(0, persona, cantMenores, tarjetaConsumo); //TODO, ver como se maneja el id
        tarjetaConsumo.setPersonaVulnerable(personaVulnerable);
        RegistroPersonaVulnerable contribucionRegistroPersonaVul = new RegistroPersonaVulnerable(fecha, tarjetaConsumo);
        

        if (!verificarColab(contribucionRegistroPersonaVul, fechaContr, colab))
        {
           throw new Exception("No tiene autorizacion para realizar esta colaboracion" );
        }

        //Se le asignan los puntos al colaborador
        colab.AgregarPuntos(AppSettings.Instance.TarjetasRepartidasCoef); 

        return contribucionRegistroPersonaVul;
    
    }

    public FormaContribucion crearDonacionMonetaria(Colaborador colab,float monto, int frecDias, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        DonacionMonetaria formaDonacionMonetaria = new DonacionMonetaria(fecha, monto, frecDias);

        if (!verificarColab(formaDonacionMonetaria, fechaContr, colab))
        {
           throw new Exception("No tiene autorizacion para realizar esta colaboracion" );
        }

        //Se le asignan los puntos al colaborador
        colab.AgregarPuntos(AppSettings.Instance.PesoDonadosCoef * monto);


        return formaDonacionMonetaria;
    }

    public FormaContribucion crearDonacionVianda(Colaborador colab,Heladera heladera, Vianda vianda, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        DonacionVianda formaDonacionVianda = new DonacionVianda(fecha, heladera, vianda);

        if (!verificarColab(formaDonacionVianda, fechaContr, colab))
        {
           throw new Exception("No tiene autorizacion para realizar esta colaboracion" );
        }

        //Se le asignan los puntos al colaborador
        colab.AgregarPuntos(AppSettings.Instance.ViandasDonadasCoef);

        return formaDonacionVianda;
    }

    public FormaContribucion crearOfertaPremio(Colaborador colab, Premio premio, DateTime? fechaContr)
    {
        var fecha = fechaContr ?? DateTime.Now;
        OfertaPremio formaOfertaPremio = new OfertaPremio(fecha, premio);

        if (!verificarColab(formaOfertaPremio, fechaContr, colab))
        {
           throw new Exception("No tiene autorizacion para realizar esta colaboracion" );
        }

        //No se asignan puntos por esta colaboracion

        return formaOfertaPremio;
    }

    public bool verificarColab(FormaContribucion formaContri, DateTime? fecha, Colaborador colab)
    {
        // return fecha != null && formaContri.EsValido(colab);
        return true;
    } 
}