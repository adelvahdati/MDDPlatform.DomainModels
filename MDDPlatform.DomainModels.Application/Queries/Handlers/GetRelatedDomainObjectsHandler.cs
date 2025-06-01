using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetRelatedDomainObjectsHandler : IQueryHandler<GetRelatedDomainObjects, List<DomainObjectTupleDto>>
{
    private readonly IDomainModelService _domainModelService;

    public GetRelatedDomainObjectsHandler(IDomainModelService domainModelService)
    {
        _domainModelService = domainModelService;
    }

    public List<DomainObjectTupleDto> Handle(GetRelatedDomainObjects query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainObjectTupleDto>> HandleAsync(GetRelatedDomainObjects query)
    {
        List<DomainObjectTupleDto> relatedObjects = new();
        foreach(var sourceType in query.SourceTypes)
        {
            var tuples = await _domainModelService.GetRelatedObjectsAsync(query.DomainModelId,sourceType,query.RelationName);
            var domainObjectTuples = tuples.Select(tuple=>DomainObjectTupleDto.Create(tuple)).ToList();
            relatedObjects.AddRange(domainObjectTuples);
        }
        return relatedObjects;
    }
}
