using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainObjetcsOfTypes : IQuery<List<DomainObjectDto>>
{
    public Guid DomainModelId {get;}
    public List<string> Types {get;}

    public GetDomainObjetcsOfTypes(Guid domainModelId, List<string> types)
    {
        this.DomainModelId = domainModelId;
        this.Types = types;
    }
}