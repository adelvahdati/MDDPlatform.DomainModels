using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers{
    public class GetAllDomainModelsHandler : IQueryHandler<GetAllDomainModels, IList<DomainModelDto>>
    {
        private IDomainModelReader _domainModelReader;

        public GetAllDomainModelsHandler(IDomainModelReader domainModelReader)
        {
            _domainModelReader = domainModelReader;
        }

        public IList<DomainModelDto> Handle(GetAllDomainModels query)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<DomainModelDto>> HandleAsync(GetAllDomainModels query)
        {
            return await _domainModelReader.GetAllDomainModelsAsync(query.DomainId);
        }
    }
}