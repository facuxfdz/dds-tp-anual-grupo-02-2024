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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Get current time
            var now = DateTime.Now;

            // Calculate next Monday
            var daysUntilNextSunday = (7 - (int)now.DayOfWeek) % 7;
            Console.WriteLine($"Days until next Sunday: {daysUntilNextSunday}");
            var nextSunday = now.Date.AddDays(daysUntilNextSunday).AddHours(0); // Midnight on Sunday

            // Calculate delay
            var timeUntilNextRun = nextSunday - now;

            // Validate the delay
            if (timeUntilNextRun < TimeSpan.Zero)
            {
                // Log additional details for debugging
                Console.WriteLine($"Now: {now}");
                Console.WriteLine($"Next Monday: {nextSunday}");
                throw new InvalidOperationException("Calculated delay is negative. Check the logic.");
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
                    Console.WriteLine($"An error occurred while running the task: {ex.Message}");
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
                Console.WriteLine("Reportes ya generados para la semana pasada");
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
            Console.WriteLine("Reportes generados para la semana pasada");
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