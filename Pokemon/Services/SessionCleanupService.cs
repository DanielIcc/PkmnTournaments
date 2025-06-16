
using Pokemon.Data;

namespace Pokemon.Services
{
    public class SessionCleanupService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IServiceProvider _serviceProvider;

        public SessionCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Ejecuta cada 1 hora
            _timer = new Timer(CleanupSessions, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        private void CleanupSessions(object? state)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var expired = db.UserSessions.Where(s => s.IsRevoked || s.ExpiresAt < DateTime.UtcNow || s.ExpiresAt == DateTime.MinValue);

            db.UserSessions.RemoveRange(expired);
            db.SaveChanges();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
