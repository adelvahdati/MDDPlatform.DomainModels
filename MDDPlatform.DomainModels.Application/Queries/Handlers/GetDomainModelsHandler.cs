using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainModelsHandler : IQueryHandler<GetDomainModels, List<DomainModelDto>>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainModelsHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public List<DomainModelDto> Handle(GetDomainModels query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainModelDto>> HandleAsync(GetDomainModels query)
    {
        Func<DomainModel, bool> predicate = dm=>dm.Domain.Id == query.DomainId;        
        List<DomainModel> domainModels = await _domainModelRepository.ListDomainModels(predicate);
        var domainModelsDto = domainModels.Select(domainModel=>DomainModelDto.CreateFrom(domainModel)).ToList();
        return domainModelsDto;
    }
}
