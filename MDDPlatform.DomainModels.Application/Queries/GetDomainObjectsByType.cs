using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainObjectsByType : IQuery<List<DomainObjectDto>>
{
    public Guid DomainModelId {get;}
    public string Type {get;}

    public GetDomainObjectsByType(Guid domainModelId, string type)
    {
        DomainModelId = domainModelId;
        Type = type;
    }
}