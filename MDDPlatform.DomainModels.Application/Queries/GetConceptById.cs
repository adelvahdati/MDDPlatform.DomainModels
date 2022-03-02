using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries{
    public class GetConceptById : IQuery<DomainConceptDto>
    {
        public Guid DomainModelId {get;}

        public Guid ConceptId {get;}
        public GetConceptById(Guid conceptId, Guid domainModelId)
        {
            ConceptId = conceptId;
            DomainModelId = domainModelId;
        }
    }
}