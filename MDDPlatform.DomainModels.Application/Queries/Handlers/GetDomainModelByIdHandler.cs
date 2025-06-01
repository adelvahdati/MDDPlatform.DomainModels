using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers
{
    public class GetDomainModelByIdHandler : IQueryHandler<GetDomainModelById, DomainModelDto>
    {
        private IDomainModelRepository _domainModelRepository;

        public GetDomainModelByIdHandler(IDomainModelRepository domainModelRepository)
        {
            _domainModelRepository = domainModelRepository;
        }

        public DomainModelDto Handle(GetDomainModelById query)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainModelDto> HandleAsync(GetDomainModelById query)
        {
            DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(query.DomainModelId);
            return DomainModelDto.CreateFrom(domainModel);
        }
    }
}