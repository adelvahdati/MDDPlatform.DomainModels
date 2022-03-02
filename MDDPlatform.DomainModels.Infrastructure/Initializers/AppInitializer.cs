using MDDPlatform.DomainModels.Services.ExternalEvents;
using MDDPlatform.Messages.Brokers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MDDPlatform.DomainModels.Infrastructure.Initializers{
    public class AppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AppInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider=serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            IMessageBroker messageBroker = scope.ServiceProvider.GetRequiredService<IMessageBroker>(); 
            await messageBroker.SubscribeAsync<ModelCreated>();
            Console.WriteLine("---> Subscribe to events");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service Stoped");
            return Task.CompletedTask;

        }
    }
}