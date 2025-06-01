using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainObjectsByTypeIdHandler : IQueryHandler<GetDomainObjectsByTypeId, List<DomainObjectDto>>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainObjectsByTypeIdHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public List<DomainObjectDto> Handle(GetDomainObjectsByTypeId query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainObjectDto>> HandleAsync(GetDomainObjectsByTypeId query)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        var domainObjects = domainModel.GetDomainObjects(query.DomainConceptId);

        return domainObjects.Select(domainObject=> DomainObjectDto.CreateFrom(domainObject)).ToList();        
    }
}