namespace MDDPlatform.DomainModels.Infrastructure.Data.Seeders;
public interface IDataSeeder
{
    Task SeedOrderModelAsync(Guid modelId);
    Task SeedProductModelAsync(Guid modelId);
    Task SeedCartModelAsync(Guid modelId);


}