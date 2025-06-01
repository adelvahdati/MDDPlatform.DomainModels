using MDDPlatform.DomainModels.Services.ExternalEvents;
using MDDPlatform.Messages.Brokers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MDDPlatform.DomainModels.Infrastructure.Initializers{
    public class AppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppInitializer> logger;

        public AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger)
        {
            _serviceProvider=serviceProvider;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            IMessageBroker messageBroker = scope.ServiceProvider.GetRequiredService<IMessageBroker>(); 
            await messageBroker.SubscribeAsync<ModelCreated>();
            logger.LogInformation("Subscribe to 'ModelCreated' Event");

            await messageBroker.SubscribeAsync<ModelRemoved>();
            logger.LogInformation("Subscribe to 'ModelRemoved' Event");

            await messageBroker.SubscribeAsync<DomainRemoved>();
            logger.LogInformation("Subscribe to 'DomainRemoved' Event");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Model Service Stoped");
            return Task.CompletedTask;

        }
    }
}