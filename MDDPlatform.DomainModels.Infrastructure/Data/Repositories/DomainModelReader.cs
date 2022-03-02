using MDDPlatform.DataStorage.MongoDB.Repositories;
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.DomainModels.Infrastructure.Data.Models;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Repositories
{
    public class DomainModelReader : IDomainModelReader
    {
        private IMongoRepository<DomainModelDocument,Guid> _repository;

        public DomainModelReader(IMongoRepository<DomainModelDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IList<DomainConceptDto>> FindConceptsByNameAsync(Guid domainModelId, string name)
        {
            
            var domainModelDocument =  await _repository.GetAsync(domainModelId);
            if(Equals(domainModelDocument,null))
                return new List<DomainConceptDto>();

            var domainConcepts = domainModelDocument.GetDomainConceptsWithName(name);
            if(domainConcepts == null)
                return new List<DomainConceptDto>();

            return domainConcepts;            
        }

        public async Task<IList<DomainConceptDto>> FindConceptsByTypeAsync(Guid domainModelId, string type)
        {
            var domainModelDocument =  await _repository.GetAsync(domainModelId);
            if(Equals(domainModelDocument,null))
                return new List<DomainConceptDto>();

            var domainConcepts = domainModelDocument.GetDomainConceptsOfType(type);
            if(domainConcepts == null)
                return new List<DomainConceptDto>();

            return domainConcepts;            
        }

        public async Task<IList<DomainConceptDto>> GetAllConceptsAsync(Guid domainModelId)
        {
            var domainModelDocument =  await _repository.GetAsync(domainModelId);
            if(Equals(domainModelDocument,null))
                return new List<DomainConceptDto>();

            var domainConcepts = domainModelDocument.GetDomainConcepts();
            if(domainConcepts == null)
                return new List<DomainConceptDto>();

            return domainConcepts;            
        }

        public async Task<IList<DomainModelDto>> GetAllDomainModelsAsync(Guid domainId)
        {
            var domainModelDocuments = await _repository.ListAsync(domainModelDocument => domainModelDocument.DomainId == domainId);
            if(domainModelDocuments.Count == 0)
                return new List<DomainModelDto>();

            return  domainModelDocuments.Select(domainModelDocument=> domainModelDocument.ToDto())
                                        .ToList();
            
        }

        public async Task<DomainConceptDto> GetConceptAsync(Guid domainModelId, string name, string type)
        {
            var domainModelDocument =  await _repository.GetAsync(domainModelId);
            if(Equals(domainModelDocument,null))
                return null;
            
            var domainConcept = domainModelDocument.GetDomainConcept(name,type);

            return domainConcept;
        }

        public async Task<DomainConceptDto> GetConceptByIdAsync(Guid domainModelId, Guid domainConceptId)
        {
            var domainModelDocument =  await _repository.GetAsync(domainModelId);
            if(Equals(domainModelDocument,null))
                return null;
            
            var domainConcept = domainModelDocument.GetDomainConceptById(domainConceptId);

            return domainConcept;
        }

        public async Task<DomainModelDto> GetDomainModelAsync(Guid domainModelId)
        {
            var domainModelDocument =  await _repository.GetAsync(domainModelId);
            if(!Equals(domainModelDocument ,null))
                return domainModelDocument.ToDto();
            
            return null;
        }
    }
}