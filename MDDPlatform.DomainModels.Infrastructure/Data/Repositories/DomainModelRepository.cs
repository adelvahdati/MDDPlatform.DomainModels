using MDDPlatform.DataStorage.MongoDB.Repositories;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Infrastructure.Data.Models;
using MDDPlatform.DomainModels.Services.Repositories;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Repositories
{
    public class DomainModelRepository : IDomainModelRepository
    {
        private IMongoRepository<DomainModelDocument,Guid> _repository;

        public DomainModelRepository(IMongoRepository<DomainModelDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task CreateDomainModelAsync(DomainModel domainModel)
        {
            await _repository.AddAsync(DomainModelDocument.CreateFrom(domainModel));
        }

        public async Task<DomainModel> GetDomainModelAsync(Guid modelId)
        {
            DomainModelDocument domainModelDoc =  await _repository.GetAsync(modelId);
            return domainModelDoc.ToCore();
        }

        public async Task UpdateDomainModelAsync(DomainModel domainModel)
        {
            await _repository.UpdateAsync(DomainModelDocument.CreateFrom(domainModel));
        }
    }
}