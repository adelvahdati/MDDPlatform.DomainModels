using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{   
    public class GetDomainModels : IQuery<List<DomainModelDto>>
    {
        public Guid DomainId {get;}

        public GetDomainModels(Guid domainId)
        {
            DomainId = domainId;
        }
    }
}