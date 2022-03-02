using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Enums;

namespace MDDPlatform.DomainModels.Application.Repositories
{
    public interface IDomainModelReader
    {
        Task<IList<DomainConceptDto>> FindConceptsByNameAsync(Guid domainModelId, string name);
        Task<IList<DomainConceptDto>> FindConceptsByTypeAsync(Guid domainModelId, string type);
        Task<IList<DomainConceptDto>> GetAllConceptsAsync(Guid domainModelId);
        Task<DomainConceptDto> GetConceptAsync(Guid domainModelId, string name, string type);
        Task<DomainConceptDto> GetConceptByIdAsync(Guid domainModelId, Guid domainConceptId);
        Task<DomainModelDto> GetDomainModelAsync(Guid domainModelId);
        Task<IList<DomainModelDto>> GetAllDomainModelsAsync(Guid domainId);
    }
}