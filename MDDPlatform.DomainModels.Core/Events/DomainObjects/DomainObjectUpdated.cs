using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events;
public class DomainObjectUpdated : IDomainEvent
{
    public Guid DomainId { get; }
    public Guid DomainConceptId { get; }
    public object DomainObjectId { get; }
    public object Name { get; }
    public object Type { get; }

    public DomainObjectUpdated(Guid domainId, Guid domainConceptId, object domainObjectId, object name, object type)
    {
        DomainId = domainId;
        DomainConceptId = domainConceptId;
        DomainObjectId = domainObjectId;
        Name = name;
        Type = type;
    }
}