using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers;
public class GetDomainObjectByIdHandler : IQueryHandler<GetDomainObjectById, DomainObjectDto>
{
    private readonly IDomainModelRepository _domainModelRepository;

    public GetDomainObjectByIdHandler(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public DomainObjectDto Handle(GetDomainObjectById query)
    {
        throw new NotImplementedException();
    }

    public async Task<DomainObjectDto> HandleAsync(GetDomainObjectById query)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
        DomainObject domainObject = domainModel.GetDomainObjectById(query.DomainObjectId);
        return DomainObjectDto.CreateFrom(domainObject);
    }
}
