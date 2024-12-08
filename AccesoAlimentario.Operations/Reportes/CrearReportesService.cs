using AccesoAlimentario.Core.DAL;
using Microsoft.Extensions.Hosting;
using AccesoAlimentario.Core.Entities.Reportes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AccesoAlimentario.Operations.Reportes
{
    public class CrearReportesService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CrearReportesService> _logger;

        public CrearReportesService(IServiceScopeFactory scopeFactory, ILogger<CrearReportesService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Get current time
            var now = DateTime.UtcNow;

            // Calculate next Monday
            var daysUntilNextSunday = (7 - (int)now.DayOfWeek) % 7;
            _logger.LogInformation($"Days until next Sunday: {daysUntilNextSunday}");
            var nextSunday = now.Date.AddDays(daysUntilNextSunday).AddHours(0); // Midnight on Sunday

            // Calculate delay
            var timeUntilNextRun = nextSunday - now;

            // Validate the delay
            if (timeUntilNextRun < TimeSpan.Zero)
            {
                timeUntilNextRun = TimeSpan.Zero;
            }
            
            var firstRun = true;
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await RunTask();
                    if (firstRun)
                    {
                        firstRun = false;
                        await Task.Delay(timeUntilNextRun, stoppingToken);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al generar reportes");
                }

                // Wait for the next run
                await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
            }
        }



        private async Task RunTask()
        {
            using var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            var today = DateTime.Today;
            var currentDay = today.DayOfWeek;
            var daysSinceLastSunday = (int)currentDay + 1;
            var endOfLastWeek = today.AddDays(-daysSinceLastSunday);
            var startOfLastWeek = endOfLastWeek.AddDays(-6);

            var reporteQuery = unitOfWork.ReporteRepository.GetQueryable();
            reporteQuery = reporteQuery.Where(r => r.FechaExpiracion < today);
            var reportes = await unitOfWork.ReporteRepository.GetCollectionAsync(reporteQuery);
            
            if (reportes.Any())
            {
                _logger.LogInformation("Reportes ya generados para la semana pasada");
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
                var reporte = await concepto.Generar(startOfLastWeek, endOfLastWeek);
                await unitOfWork.ReporteRepository.AddAsync(reporte);
            }
            
            await unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Reportes generados para la semana pasada");
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            return base.StopAsync(stoppingToken);
        }
    }
}