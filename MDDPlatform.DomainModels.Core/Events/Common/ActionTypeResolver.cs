using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events;
public class ActionTypeResolver
{
    public static ActionType Resolve(IDomainEvent @event)
    {
        return @event.GetType().Name switch
        {
            nameof(DomainObjectCreated) => ActionType.DomainObjectCreated,
            nameof(DomainObjectRemoved) => ActionType.DomainObjectRemoved,
            nameof(DomainObjectUpdated) => ActionType.DomainObjectUpdated,
            _ => ActionType.Unknown
        }; 

            
    }

}