using MDDPlatform.DomainModels.Core.Events;

namespace MDDPlatform.DomainModels.Services.Intefaces;
public interface IDomainModelNotificationService
{
    Task DomainModelUpdatedAsync(DomainModelUpdated @event);
    Task ConceptBasedModelClearedAsync(Guid domainModelId,string name,string type,string tag);
    Task DomainModelClearedAsync(Guid domainModelId,string name,string type,string tag);

}