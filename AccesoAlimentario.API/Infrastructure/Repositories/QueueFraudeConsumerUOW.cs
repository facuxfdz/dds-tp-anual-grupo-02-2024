using AccesoAlimentario.API.Domain.Heladeras;
using AccesoAlimentario.API.Domain.Heladeras.RegistrosSensores;
using AccesoAlimentario.API.Infrastructure.DAL;

namespace AccesoAlimentario.API.Infrastructure.Repositories;

public class QueueFraudeConsumerUOW : IQueueFraudeConsumerUOW
{
      private readonly AppDbContext _context;
        private GenericRepository<Heladera> _heladeraRepository;
        private GenericRepository<RegistroFraude> _registroFraudeRepository;
    
        public QueueFraudeConsumerUOW(AppDbContext context)
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
    
        public IRepository<RegistroFraude> RegistroFraudeRepository
        {
            get
            {
                if (_registroFraudeRepository == null)
                {
                    _registroFraudeRepository = new GenericRepository<RegistroFraude>(_context);
                }
                return _registroFraudeRepository;
            }
        }
    
        public void Complete()
        {
            _context.SaveChanges();
        }
}