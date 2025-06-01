using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{
    public class GetDomainObjectsByTypeId : IQuery<List<DomainObjectDto>>
    {
        public Guid DomainModelId {get;}
        public Guid DomainConceptId {get;}

        public GetDomainObjectsByTypeId(Guid domainModelId, Guid domainConceptId)
        {
            DomainModelId = domainModelId;
            DomainConceptId = domainConceptId;
        }
    }
}