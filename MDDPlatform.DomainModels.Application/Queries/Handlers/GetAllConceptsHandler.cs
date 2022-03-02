using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers
{
    public class GetAllConceptsHandler : IQueryHandler<GetAllConcepts, IList<DomainConceptDto>>
    {
        private IDomainModelReader _domainModelReader;

        public GetAllConceptsHandler(IDomainModelReader domainModelReader)
        {
            _domainModelReader = domainModelReader;
        }

        public IList<DomainConceptDto> Handle(GetAllConcepts query)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<DomainConceptDto>> HandleAsync(GetAllConcepts query)
        {   
            return await _domainModelReader.GetAllConceptsAsync(query.DomainModelId);
        }
    }
}