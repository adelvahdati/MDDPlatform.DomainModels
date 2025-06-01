using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainObjectById : IQuery<DomainObjectDto>{
    public Guid DomainModelId {get;}

    public Guid DomainObjectId {get;}

    public GetDomainObjectById(Guid domainModelId, Guid domainObjectId)
    {
        DomainModelId = domainModelId;
        DomainObjectId = domainObjectId;
    }
}