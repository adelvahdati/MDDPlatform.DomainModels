using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events;
public class DomainModelUpdated : IDomainEvent
{
    public Guid DomainId {get;}
    public Guid DomainModelId {get;}
    public string Name {get;}
    public string Type {get;}
    public string Tag {get;}
    public EventPayload Payload {get;}

    public DomainModelUpdated(Guid domainId, Guid domainModelId, string name, string type, string tag, EventPayload payload)
    {
        DomainId = domainId;
        DomainModelId = domainModelId;
        Name = name;
        Type = type;
        Tag = tag;
        Payload = payload;
    }
}