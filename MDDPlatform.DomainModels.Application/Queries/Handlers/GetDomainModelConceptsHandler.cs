using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainModelConceptsHandler : IQueryHandler<GetDomainModelConcepts, List<ConceptDto>>{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainModelConceptsHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public List<ConceptDto> Handle(GetDomainModelConcepts query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ConceptDto>> HandleAsync(GetDomainModelConcepts query)
    {
       DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
       var concepts =  domainModel.Concepts;

       List<ConceptDto> conceptsDto = concepts.Select(concept=> ConceptDto.CreateFrom(concept)).ToList();
       return conceptsDto;
    }
}
