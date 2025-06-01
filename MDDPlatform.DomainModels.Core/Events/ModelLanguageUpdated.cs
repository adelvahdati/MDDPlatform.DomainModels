using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events;
public class ModelLanguageUpdated : IDomainEvent{
    public Guid ModelId {get;set;}
    public Guid LanguageId {get;set;}

    public ModelLanguageUpdated(Guid modelId, Guid languageId)
    {
        ModelId = modelId;
        LanguageId = languageId;
    }
}