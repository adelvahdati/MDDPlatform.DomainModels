using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainConceptsByFullNamesHandler : IQueryHandler<GetDomainConceptsByFullNames, List<DomainConceptDto>>{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainConceptsByFullNamesHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public List<DomainConceptDto> Handle(GetDomainConceptsByFullNames query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainConceptDto>> HandleAsync(GetDomainConceptsByFullNames query)
    {
        var domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        
        List<DomainConcept> domainConcepts = domainModel.GetDomainConcepts(query.FullNames);

        return domainConcepts.Select(domainConcept=> DomainConceptDto.CreateFrom(domainConcept)).ToList();
    }
}
