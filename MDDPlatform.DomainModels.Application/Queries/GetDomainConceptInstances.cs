using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainConceptInstances : IQuery<List<InstanceDto>>
{
    public Guid DomainModelId {get;}
    public Guid DomainConceptId {get;}

    public GetDomainConceptInstances(Guid domainModelId, Guid domainConceptId)
    {
        DomainModelId = domainModelId;
        DomainConceptId = domainConceptId;
    }
}