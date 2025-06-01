using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Services.ExternalEvents;
public class ModelRemoved : IDomainEvent
{
    public Guid DomainId {get;set;}
    public Guid ModelId {get;set;}

    public ModelRemoved(Guid domainId, Guid modelId)
    {
        DomainId = domainId;
        ModelId = modelId;
    }
}