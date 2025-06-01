using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events;
public class DomainModelRemoved : IDomainEvent
{
    public Guid DomainId {get;set;}
    public Guid DomainModelId {get;set;}

    public DomainModelRemoved(Guid domainId, Guid domainModelId)
    {
        DomainId = domainId;
        DomainModelId = domainModelId;
    }
}