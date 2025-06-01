using System.Linq.Expressions;
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

        public async Task DeleteDomainModelAsync(Guid DomainModelId)
        {
            await _repository.DeleteAsync(DomainModelId);
        }

        public async Task<bool> DeleteDomainModelAsync(Guid DomainId, Guid DomainModelId)
        {
            var result = await _repository.DeleteAsync(domainModel=>domainModel.DomainId == DomainId && domainModel.Id== DomainModelId);
            return result;
        }

        public async Task<DomainModel> GetDomainModelAsync(Guid modelId)
        {
            DomainModelDocument domainModelDoc =  await _repository.GetAsync(modelId);
            return domainModelDoc.ToDomainModel();

        }

        public async Task<List<DomainModel>> ListDomainModels(Func<DomainModel, bool> predicate)
        {
            Expression<Func<DomainModelDocument,bool>> predicateExpression = domaninModelDocument => predicate(domaninModelDocument.ToDomainModel());
            var items = await _repository.ListAsync(predicateExpression);
            return items.Select(domainModelDoc=> domainModelDoc.ToDomainModel()).ToList();
        }

        public async Task UpdateDomainModelAsync(DomainModel domainModel)
        {
            await _repository.UpdateAsync(DomainModelDocument.CreateFrom(domainModel));
        }
    }
}