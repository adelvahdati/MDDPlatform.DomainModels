using MDDPlatform.DataStorage.MongoDB;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Infrastructure.Data.Models;
using MDDPlatform.DomainModels.Infrastructure.Data.Repositories;
using MDDPlatform.DomainModels.Infrastructure.Data.Seeders;
using MDDPlatform.DomainModels.Infrastructure.Initializers;
using MDDPlatform.DomainModels.Infrastructure.Services;
using MDDPlatform.DomainModels.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Extensions.Handlers;
using MDDPlatform.RestClients;
using MDDPlatform.SharedKernel.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MDDPlatform.DomainModels.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration){
            
            services.AddHandlers();
            services.AddHostedService<AppInitializer>();
            services.AddRabbitMQ(configuration,"rabbitmq");
            services.AddMongoDB(configuration,"mongodb");
            services.AddMongoRespository<DomainModelDocument,Guid>("DomainModels");

            services.AddSingleton<IEventMapper,DefaultEventMapper>();

            services.AddScoped<IDomainModelRepository, DomainModelRepository>();

            services.AddScoped<IDomainModelService,DomainModelService>();
            services.AddScoped<IModelOperationService,ModelOperationService>();

            services.AddHttpClient<IRestClient,RestClient>
            (
                httpClient =>
                {
                    var url = configuration["Services:ConceptService"];
                    httpClient.BaseAddress = new Uri(url);
                }
            );
            
            services.AddScoped<IConceptService,ConceptService>();
            services.AddScoped<ILanguageService,LanguageService>();

            services.AddScoped<IDataSeeder,DataSeeder>();

            return services;
        }
    }
}