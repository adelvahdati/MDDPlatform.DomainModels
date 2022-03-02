using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Enums;

namespace MDDPlatform.DomainModels.Application.Services{
    public interface IDomainModelService 
    {
        Task CreateConceptAsync(string name, string type,Guid domainModelId);
        Task RemoveConceptAsync(string name, string type,Guid domainModelId);
        Task<IList<DomainConceptDto>> FindConceptsByNameAsync(Guid domainModelId, string name);
        Task<IList<DomainConceptDto>> FindConceptsByTypeAsync(Guid domainModelId,string type);
        Task<IList<DomainConceptDto>> GetAllConceptsAsync(Guid domainModelId);
        Task<DomainConceptDto> GetConceptAsync(string name, string type, Guid domainModelId);
        Task<DomainConceptDto> GetConceptByIdAsync(Guid conceptId, Guid domainModelId);
        Task<DomainModelDto> GetDomainModelAsync(Guid domainModelId);
        Task<IList<DomainModelDto>> GetAllDomainModelsAsync(Guid domainId);
    }
}