using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainConceptByFullNameHandler : IQueryHandler<GetDomainConceptByFullName, DomainConceptDto>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainConceptByFullNameHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public DomainConceptDto Handle(GetDomainConceptByFullName query)
    {
        throw new NotImplementedException();
    }

    public async Task<DomainConceptDto> HandleAsync(GetDomainConceptByFullName query)
    {
        DomainModel domainModel =await  _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        if(Equals(domainModel,null))
            throw new Exception("Domain Model not found");
        
        DomainConcept? domainConcept = domainModel.GetDomainConcept(ConceptFullName.Create(query.FullName));
        if(Equals(domainConcept,null))
            throw new Exception("The model does not contain this concept");

        return DomainConceptDto.CreateFrom(domainConcept);
    }
}