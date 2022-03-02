using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Repositories;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries.Handlers{
    public class GetConceptByIdHandler : IQueryHandler<GetConceptById, DomainConceptDto>
    {
        private IDomainModelReader _domainModelReader;

        public GetConceptByIdHandler(IDomainModelReader domainModelReader)
        {
            _domainModelReader = domainModelReader;
        }

        public DomainConceptDto Handle(GetConceptById query)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainConceptDto> HandleAsync(GetConceptById query)
        {
            return await _domainModelReader.GetConceptByIdAsync(query.DomainModelId,query.ConceptId);
        }
    }
}