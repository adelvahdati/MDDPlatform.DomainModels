using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainObjects : IQuery<List<DomainObjectDto>>
{
    public Guid DomainModelId {get;}

    public GetDomainObjects(Guid domainModelId)
    {
        this.DomainModelId = domainModelId;
    }
}