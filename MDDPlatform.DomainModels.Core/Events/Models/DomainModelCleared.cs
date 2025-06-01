using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events;
public class DomainModelCleared : IDomainEvent
{
    public Guid DomainId {get;}
    public Guid DomainModelId {get;}
    public string Name {get;}
    public string Type {get;}
    public string Tag {get;}

    public DomainModelCleared(Guid domainId, Guid domainModelId, string name, string type, string tag)
    {
        DomainId = domainId;
        DomainModelId = domainModelId;
        Name = name;
        Type = type;
        Tag = tag;
    }
}