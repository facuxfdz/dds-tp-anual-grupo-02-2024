using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;
using AccesoAlimentario.API.Infrastructure.DAL;

namespace AccesoAlimentario.API.Infrastructure.Repositories;

public class QueueTemperaturaConsumerUOW : IQueueTemperaturaConsumerUOW
{
    private readonly AppDbContext _context;
    private GenericRepository<Heladera> _heladeraRepository;
    private GenericRepository<RegistroTemperatura> _registroTemperaturaRepository;

    public QueueTemperaturaConsumerUOW(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<Heladera> HeladeraRepository
    {
        get
        {
            if (_heladeraRepository == null)
            {
                _heladeraRepository = new GenericRepository<Heladera>(_context);
            }
            return _heladeraRepository;
        }
    }

    public IRepository<RegistroTemperatura> RegistroTemperaturaRepository
    {
        get
        {
            if (_registroTemperaturaRepository == null)
            {
                _registroTemperaturaRepository = new GenericRepository<RegistroTemperatura>(_context);
            }
            return _registroTemperaturaRepository;
        }
    }

    public void Complete()
    {
        _context.SaveChanges();
    }
}