using MDDPlatform.DomainModels.Api.Hubs;
using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Services.Intefaces;
using Microsoft.AspNetCore.SignalR;

namespace MDDPlatform.DomainModels.Api.Services;
public class DomainModelNotificationService : IDomainModelNotificationService
{
    private IHubContext<DomainModelHub> _domainModelHub;

    public DomainModelNotificationService(IHubContext<DomainModelHub> domainModelHub)
    {
        _domainModelHub = domainModelHub;
    }

    public async Task ConceptBasedModelClearedAsync(Guid domainModelId, string name, string type, string tag)
    {
        await _domainModelHub.Clients.All.SendAsync("ConceptBasedModelCleared",domainModelId,name,type,tag);
    }

    public async Task DomainModelClearedAsync(Guid domainModelId, string name, string type, string tag)
    {
        await _domainModelHub.Clients.All.SendAsync("DomainModelCleared",domainModelId,name,type,tag);
    }

    public async Task DomainModelUpdatedAsync(DomainModelUpdated @event)
    {
        await _domainModelHub.Clients.All.SendAsync("DomainModelUpdated",@event);
        
    }        
}