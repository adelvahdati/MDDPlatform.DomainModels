using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;

public class GetDomainConceptByIdHandler : IQueryHandler<GetDomainConceptById, DomainConceptDto>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainConceptByIdHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public DomainConceptDto Handle(GetDomainConceptById query)
    {
        throw new NotImplementedException();
    }

    public async Task<DomainConceptDto> HandleAsync(GetDomainConceptById query)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        DomainConcept? domainConcept = domainModel.GetDomainConceptById(query.DomainConceptId);
        if(Equals(domainConcept,null))
            throw new Exception("Invalid domain concept Id");
        return DomainConceptDto.CreateFrom(domainConcept);
        
    }
}