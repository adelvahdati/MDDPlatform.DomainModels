using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers{
    public class GetDomainModelHandler : IQueryHandler<GetDomainModel, DomainModelDto>
    {
        private IDomainModelReader _domainModelReader;

        public GetDomainModelHandler(IDomainModelReader domainModelReader)
        {
            _domainModelReader = domainModelReader;
        }

        public DomainModelDto Handle(GetDomainModel query)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainModelDto> HandleAsync(GetDomainModel query)
        {
            return await _domainModelReader.GetDomainModelAsync(query.DomainModelId);
        }
    }
}