using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{   
    public class GetAllDomainModels : IQuery<IList<DomainModelDto>>
    {
        public Guid DomainId {get;}

        public GetAllDomainModels(Guid domainId)
        {
            DomainId = domainId;
        }
    }
}