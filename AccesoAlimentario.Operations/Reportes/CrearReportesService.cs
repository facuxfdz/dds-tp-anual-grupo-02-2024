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
            // Get current time
            var now = DateTime.Now;

            // Calculate next Monday
            var daysUntilNextMonday = (7 - (int)now.DayOfWeek) + 1;
            Console.WriteLine($"Days until next Monday: {daysUntilNextMonday}");
            var nextMonday = now.Date.AddDays(daysUntilNextMonday).AddHours(0); // Midnight on Monday

            // Calculate delay
            var timeUntilNextRun = nextMonday - now;

            // Validate the delay
            if (timeUntilNextRun < TimeSpan.Zero)
            {
                // Log additional details for debugging
                Console.WriteLine($"Now: {now}");
                Console.WriteLine($"Next Monday: {nextMonday}");
                throw new InvalidOperationException("Calculated delay is negative. Check the logic.");
            }

            // Set up the timer to execute weekly
            _timer = new Timer(RunTask, null, timeUntilNextRun, TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }



        private async void RunTask(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            var today = DateTime.Today;
            var currentDay = today.DayOfWeek;
            var daysSinceLastSunday = (int)currentDay + 1;
            var endOfLastWeek = today.AddDays(-daysSinceLastSunday);
            var startOfLastWeek = endOfLastWeek.AddDays(-6);

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