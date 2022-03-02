using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers{
    public class GetConceptHandler : IQueryHandler<GetConcept, DomainConceptDto>
    {
        private IDomainModelReader _domainModelReader;

        public GetConceptHandler(IDomainModelReader domainModelReader)
        {
            _domainModelReader = domainModelReader;
        }

        public DomainConceptDto Handle(GetConcept query)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainConceptDto> HandleAsync(GetConcept query)
        {
            return await _domainModelReader.GetConceptAsync(query.DomainModelId,query.Name,query.Type);
        }
    }
}