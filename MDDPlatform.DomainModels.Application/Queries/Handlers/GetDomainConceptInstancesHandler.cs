using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainConceptInstancesHandler : IQueryHandler<GetDomainConceptInstances, List<InstanceDto>>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainConceptInstancesHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public List<InstanceDto> Handle(GetDomainConceptInstances query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<InstanceDto>> HandleAsync(GetDomainConceptInstances query)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);        
        var instances = domainModel.GetDomainConceptInstances(query.DomainConceptId);
        List<InstanceDto> instancesDto = instances.Select(instance=> InstanceDto.CreateFrom(instance)).ToList();
        return instancesDto;
    }
}
