using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Queries;
using MDDPlatform.DomainModels.Services.Commands;
using MDDPlatform.Messages.Dispatchers;

namespace MDDPlatform.DomainModels.Application.Services
{
    public class DomainModelService : IDomainModelService
    {
        private readonly IMessageDispatcher _messageDispatcher;

        public DomainModelService(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
        }

        public async Task CreateConceptAsync(string name, string type, Guid domainModelId)
        {
            CreateConcept command = new CreateConcept(name,type,domainModelId);
            await _messageDispatcher.HandleAsync(command);
        }
        public async Task RemoveConceptAsync(string name,string type,Guid domainModelId)
        {
            RemoveConcept command = new RemoveConcept(domainModelId,name,type);
            await _messageDispatcher.HandleAsync(command);
        }

        public async Task<IList<DomainConceptDto>> FindConceptsByNameAsync(Guid domainModelId, string name)
        {
            FindConceptsByName query = new FindConceptsByName(domainModelId,name);
            return await _messageDispatcher.HandleAsync<IList<DomainConceptDto>>(query);
        }

        public async Task<IList<DomainConceptDto>> FindConceptsByTypeAsync(Guid domainModelId, string type)
        {
            FindConceptsByType query = new FindConceptsByType(domainModelId,type);
            return await _messageDispatcher.HandleAsync<IList<DomainConceptDto>>(query);
        }

        public async Task<IList<DomainConceptDto>> GetAllConceptsAsync(Guid domainModelId)
        {
            GetAllConcepts query = new GetAllConcepts(domainModelId);
            return await _messageDispatcher.HandleAsync<IList<DomainConceptDto>>(query);
        }

        public async Task<DomainConceptDto> GetConceptAsync(string name, string type, Guid domainModelId)
        {
            GetConcept query = new GetConcept(name,type,domainModelId);
            return await _messageDispatcher.HandleAsync<DomainConceptDto>(query);
        }

        public async Task<DomainConceptDto> GetConceptByIdAsync(Guid conceptId, Guid domainModelId)
        {
            GetConceptById query = new GetConceptById(conceptId,domainModelId);
            return await _messageDispatcher.HandleAsync<DomainConceptDto>(query);
        }

        public async Task<DomainModelDto> GetDomainModelAsync(Guid domainModelId)
        {
            GetDomainModel query = new GetDomainModel(domainModelId);
            return await _messageDispatcher.HandleAsync<DomainModelDto>(query);
        }

        public async Task<IList<DomainModelDto>> GetAllDomainModelsAsync(Guid domainId)
        {
            GetAllDomainModels query = new GetAllDomainModels(domainId);
            return await _messageDispatcher.HandleAsync<IList<DomainModelDto>>(query);
        }
    }
}