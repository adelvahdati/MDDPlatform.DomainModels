using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class RemoveDomainObject : ICommand
{
    public Guid DomainModelId {get; set;}
    public Guid DomainObjectId {get;set;}

    public RemoveDomainObject(Guid domainModelId,Guid domainObjectId)
    {
        DomainObjectId = domainObjectId;
        DomainModelId = domainModelId;
    }
}