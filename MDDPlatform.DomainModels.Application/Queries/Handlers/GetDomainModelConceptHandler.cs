using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainModelConceptHandler : IQueryHandler<GetDomainModelConcept, ConceptDto>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainModelConceptHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public ConceptDto Handle(GetDomainModelConcept query)
    {
        throw new NotImplementedException();
    }

    public async Task<ConceptDto> HandleAsync(GetDomainModelConcept query)
    {
        DomainModel domainModel =await  _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        DomainConcept? domainConcept = domainModel.GetDomainConcept(ConceptFullName.Create(query.FullName));
        if(Equals(domainConcept,null))
            throw new Exception("DomainConcept not found");
            
        return ConceptDto.CreateFrom(domainConcept);
    }
}
