using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetModelElementsHandler : IQueryHandler<GetModelElements, List<ElementDto>>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetModelElementsHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public List<ElementDto> Handle(GetModelElements query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ElementDto>> HandleAsync(GetModelElements query)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        if(Equals(domainModel,null))
            return new();
        
        var domainConcepts = domainModel.DomainConcepts;
        var elements = domainConcepts.Select(c => ElementDto.CreateFrom(c)).ToList();
        return elements;
    }
}
