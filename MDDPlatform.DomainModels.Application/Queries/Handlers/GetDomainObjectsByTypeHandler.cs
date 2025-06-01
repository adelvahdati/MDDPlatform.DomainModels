using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainObjectsByTypeHandler : IQueryHandler<GetDomainObjectsByType, List<DomainObjectDto>>
{
    private readonly IDomainModelService _domainModelService;

    public GetDomainObjectsByTypeHandler(IDomainModelService domainModelService)
    {
        _domainModelService = domainModelService;
    }

    public List<DomainObjectDto> Handle(GetDomainObjectsByType query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainObjectDto>> HandleAsync(GetDomainObjectsByType query)
    {
        var domainObjects = await _domainModelService.GetDomainObjectsByTypeAsync(query.DomainModelId,query.Type);
        return domainObjects;
    }
}