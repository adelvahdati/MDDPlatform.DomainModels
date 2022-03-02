using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Services.Repositories {
    public interface IDomainModelRepository
    {
        Task<DomainModel> GetDomainModelAsync(Guid modelId);
        Task CreateDomainModelAsync(DomainModel domainModel);
        Task UpdateDomainModelAsync(DomainModel domainModel);
    }
}