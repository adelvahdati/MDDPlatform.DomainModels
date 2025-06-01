using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainModelElementsHandler : IQueryHandler<GetDomainModelElements, DomainModelElementsDto?>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainModelElementsHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public DomainModelElementsDto Handle(GetDomainModelElements query)
    {
        throw new NotImplementedException();
    }

    public async Task<DomainModelElementsDto?> HandleAsync(GetDomainModelElements query)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        if(Equals(domainModel,null))
            return null;
            
        return DomainModelElementsDto.CreateFrom(domainModel);                
    }
}
