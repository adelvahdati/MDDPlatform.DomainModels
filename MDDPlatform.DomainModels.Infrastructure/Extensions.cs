using MDDPlatform.DataStorage.MongoDB;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Infrastructure.Data.Models;
using MDDPlatform.DomainModels.Infrastructure.Data.Repositories;
using MDDPlatform.DomainModels.Infrastructure.Initializers;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Extensions.Handlers;
using MDDPlatform.SharedKernel.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MDDPlatform.DomainModels.Infrastructure{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration){
            
            services.AddHandlers();
            services.AddHostedService<AppInitializer>();
            services.AddRabbitMQ(configuration,"rabbitmq");
            services.AddMongoDB(configuration,"mongodb");
            services.AddMongoRespository<DomainModelDocument,Guid>("DomainModels");

            services.AddSingleton<IEventMapper,DefaultEventMapper>();
            services.AddSingleton<IDomainModelService,DomainModelService>();
            services.AddScoped<IDomainModelRepository, DomainModelRepository>();
            services.AddScoped<IDomainModelReader,DomainModelReader>();


            return services;
        }
    }
}