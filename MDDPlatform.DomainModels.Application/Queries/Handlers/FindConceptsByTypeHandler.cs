using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers{
    public class FindConceptsByTypeHandler : IQueryHandler<FindConceptsByType, IList<DomainConceptDto>>
    {
        private IDomainModelReader _domainModelReader;

        public FindConceptsByTypeHandler(IDomainModelReader domainModelReader)
        {
            _domainModelReader = domainModelReader;
        }

        public IList<DomainConceptDto> Handle(FindConceptsByType query)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<DomainConceptDto>> HandleAsync(FindConceptsByType query)
        {
            return await _domainModelReader.FindConceptsByTypeAsync(query.DomainModelId,query.Type);
        }
    }
}