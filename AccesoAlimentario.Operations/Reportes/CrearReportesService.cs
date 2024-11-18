using AccesoAlimentario.Core.DAL;
using Microsoft.Extensions.Hosting;
using AccesoAlimentario.Core.Entities.Reportes;
using Microsoft.Extensions.DependencyInjection;

namespace AccesoAlimentario.Operations.Reportes
{
    public class CrearReportesService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public CrearReportesService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Calcula el tiempo inicial de espera para el próximo lunes a las 00:00
            var now = DateTime.Now;
            var nextMonday = now.AddDays((int)DayOfWeek.Monday - (int)now.DayOfWeek + (now.DayOfWeek == DayOfWeek.Monday && now.TimeOfDay < TimeSpan.FromHours(24) ? 0 : 7));
            var timeUntilNextRun = nextMonday.Date.AddHours(0) - now;
            if (timeUntilNextRun < TimeSpan.Zero)
            {
                timeUntilNextRun = TimeSpan.Zero;
            }
            // Configura el timer para que se ejecute cada semana (7 días)
            _timer = new Timer(RunTask, null, timeUntilNextRun, TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }

        private async void RunTask(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            var today = DateTime.Today;
            var currentDayOfWeek = (int)today.DayOfWeek;
            var startOfWeek = today.AddDays(-currentDayOfWeek);
            var endOfWeek = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
            
            var lastReports = await unitOfWork.ReporteRepository.GetCollectionAsync(
                unitOfWork.ReporteRepository.GetQueryable()
            );
            
            var lastReport = lastReports.OrderByDescending(r => r.FechaCreacion).FirstOrDefault();
            
            if (lastReport != null && lastReport.FechaCreacion.Date >= startOfWeek.Date)
            {
                return;
            }

            List<IReporteBuilder> conceptos =
            [
                new ReporteBuilderHeladeraFallas(unitOfWork),
                new ReporteBuilderColaboradorViandasDonadas(unitOfWork),
                new ReporteBuilderHeladeraCambioViandas(unitOfWork),
            ];

            foreach (var concepto in conceptos)
            {
                var reporte = await concepto.Generar(startOfWeek, endOfWeek);
                await unitOfWork.ReporteRepository.AddAsync(reporte);
            }
            
            await unitOfWork.SaveChangesAsync();
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(stoppingToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}