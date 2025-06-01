using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainObjectsHandler : IQueryHandler<GetDomainObjects, List<DomainObjectDto>>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainObjectsHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public List<DomainObjectDto> Handle(GetDomainObjects query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainObjectDto>> HandleAsync(GetDomainObjects query)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        var domainObjects = domainModel.GetDomainObjects();

        return domainObjects.Select(domainObject=> DomainObjectDto.CreateFrom(domainObject)).ToList();        
    }
}
