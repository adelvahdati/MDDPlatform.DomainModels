using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application.Services;
public interface IDomainModelService 
{
    ///////////////////////////////// Query ///////////////////////////////////////////////
    Task<List<DomainObjectDto>> GetDomainObjectsByTypeAsync(Guid domainModelId, string type);
    Task<List<DomainObjectDto>> GetDomainObjectsOfTypesAsync(Guid domainModelId, List<string> types);
    Task<List<Tuple<DomainObject,string,DomainObject>>> GetRelatedObjectsAsync(Guid domainModelId,string sourceType,string relationName,string targetType);
    Task<List<Tuple<DomainObject,string,DomainObject>>> GetRelatedObjectsAsync(Guid domainModelId,string sourceType,string relationName);
    Task<List<Tuple<DomainObject,string,List<DomainObject>>>> GetRelatedObjectsListAsync(Guid domainModelId,string sourceType,string relationName);
    Task<List<Tuple<DomainObject,string,DomainObject,string,List<DomainObject>>>> GetChainOfRelatedObjectAsync(Guid domainModelId,string startNode, string start2MiddleRelation, string middleNode,string middle2LastNodeRelation);
    Task<List<Tuple<DomainObject,string,DomainObject,string,DomainObject>>> GetChainOfRelatedObjectAsync(Guid inputModel, string firstNode, string firstToMiddleNodeRelation, string middleNode, string middleToLastNodeRelation, string lastNode);
}
