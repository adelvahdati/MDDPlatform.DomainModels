using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{   
    public class GetDomainModel : IQuery<DomainModelDto>
    {
        public Guid DomainModelId {get;}

        public GetDomainModel(Guid domainModelId)
        {
            DomainModelId = domainModelId;
        }
    }
}