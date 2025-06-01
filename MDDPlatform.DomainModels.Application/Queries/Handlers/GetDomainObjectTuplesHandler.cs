using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainObjectTuplesHandler : IQueryHandler<GetDomainObjectTuples, List<DomainObjectTupleDto>>
{

    private readonly IDomainModelService _domainModelService;

    public GetDomainObjectTuplesHandler(IDomainModelService domainModelService)
    {
        _domainModelService = domainModelService;
    }

    public List<DomainObjectTupleDto> Handle(GetDomainObjectTuples query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainObjectTupleDto>> HandleAsync(GetDomainObjectTuples query)
    {
        var tuples = await _domainModelService.GetRelatedObjectsAsync(query.DomainModelId,query.SourceType,query.RelationName,query.TargetType);
        var domainObjectTuples = tuples.Select(tuple=>DomainObjectTupleDto.Create(tuple)).ToList();
        return domainObjectTuples;
    }
}
