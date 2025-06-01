using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainObjetcsOfTypesHander : IQueryHandler<GetDomainObjetcsOfTypes, List<DomainObjectDto>>
{
    private readonly IDomainModelService _domainModelService;

    public GetDomainObjetcsOfTypesHander(IDomainModelService domainModelService)
    {
        _domainModelService = domainModelService;
    }

    public List<DomainObjectDto> Handle(GetDomainObjetcsOfTypes query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainObjectDto>> HandleAsync(GetDomainObjetcsOfTypes query)
    {
        return await _domainModelService.GetDomainObjectsOfTypesAsync(query.DomainModelId,query.Types);
    }
}