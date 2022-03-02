using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers{
    public class FindConceptsByNameHandler : IQueryHandler<FindConceptsByName, IList<DomainConceptDto>>
    {
        private IDomainModelReader _domainModelReader;

        public FindConceptsByNameHandler(IDomainModelReader domainModelReader)
        {
            _domainModelReader = domainModelReader;
        }

        public IList<DomainConceptDto> Handle(FindConceptsByName query)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<DomainConceptDto>> HandleAsync(FindConceptsByName query)
        {
            return await _domainModelReader.FindConceptsByNameAsync(query.DomainModelId,query.Name);
        }
    }
}