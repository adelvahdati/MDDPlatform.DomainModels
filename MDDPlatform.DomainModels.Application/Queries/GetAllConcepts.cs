using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{
    public class GetAllConcepts : IQuery<IList<DomainConceptDto>>
    {
        public Guid DomainModelId {get;}

        public GetAllConcepts(Guid domainModelId)
        {
            DomainModelId = domainModelId;
        }
    }
}