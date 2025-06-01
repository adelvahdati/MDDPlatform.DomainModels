using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class ClearDomainModel : ICommand{
    public Guid DomainModelId {get; set;}

    public ClearDomainModel(Guid domainModelId)
    {
        DomainModelId = domainModelId;
    }
}