using System.Reflection;
using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events;
public class EventPayload
{
    public ActionType Action {get;}
    public Dictionary<string,object?> Result {get;}
    public string EventType {get;}

    private EventPayload(ActionType action, Dictionary<string, object?> result, string eventType)
    {
        Action = action;
        Result = result;
        EventType = eventType;
    }

    public static EventPayload Create(IDomainEvent @event)
    {
        var actionType = ActionTypeResolver.Resolve(@event);

        Dictionary<string,object?> payload = new();
        var properties = @event.GetType().GetProperties(BindingFlags.Public);
        foreach(var propInfo in properties){
            var value = propInfo.GetValue(@event);
            payload.Add(propInfo.Name,value);
        }

        return new EventPayload(actionType,payload,@event.GetType().Name);
    }    
}