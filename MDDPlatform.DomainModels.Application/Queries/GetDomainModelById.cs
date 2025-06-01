
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{   
    public class GetDomainModelById : IQuery<DomainModelDto>
    {
        public Guid DomainModelId {get;}

        public GetDomainModelById(Guid domainModelId)
        {
            DomainModelId = domainModelId;
        }
    }
}