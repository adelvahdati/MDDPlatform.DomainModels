using MDDPlatform.DomainModels.Core.Events;
using Microsoft.AspNetCore.SignalR;

namespace MDDPlatform.DomainModels.Api.Hubs;
public class DomainModelHub : Hub
{
    public async Task ConceptBasedModelClearedAsync(Guid domainModelId, string name, string type, string tag)
    {
        await Clients.All.SendAsync("ConceptBasedModelCleared",domainModelId,name,type,tag);
    }

    public async Task DomainModelClearedAsync(Guid domainModelId, string name, string type, string tag)
    {
        await Clients.All.SendAsync("DomainModelCleared",domainModelId,name,type,tag);
    }

    public async Task DomainModelUpdatedAsync(DomainModelUpdated @event)
    {
        await Clients.All.SendAsync("DomainModelUpdated",@event);
    }        
}