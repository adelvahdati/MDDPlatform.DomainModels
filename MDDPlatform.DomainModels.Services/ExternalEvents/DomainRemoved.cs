using MDDPlatform.Messages.Events;

namespace MDDPlatform.DomainModels.Services.ExternalEvents;
public class DomainRemoved : IEvent
{
    public Guid DomainId {get;set;}
    public List<Guid> ModelIds {get;set;}

    public DomainRemoved(Guid domainId, List<Guid> modelIds)
    {
        DomainId = domainId;
        ModelIds = modelIds;
    }
}