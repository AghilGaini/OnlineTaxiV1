using CoreService;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace WebPanel.Services
{
    public class StartupService : IHostedService
    {
        private readonly IServiceScopeFactory _services;

        public StartupService(IServiceScopeFactory services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var service = _services.CreateScope())
            {
                var unitOfWork = service.ServiceProvider.GetRequiredService<IUnitOfWork>();
                await PermisionManager.SetPermisions(unitOfWork);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
