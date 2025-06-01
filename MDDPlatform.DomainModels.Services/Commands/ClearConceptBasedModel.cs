using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class ClearConceptBasedModel : ICommand
{
    public Guid DomainModelId {get;set;}

    public ClearConceptBasedModel(Guid domainModelId)
    {
        DomainModelId = domainModelId;
    }
}