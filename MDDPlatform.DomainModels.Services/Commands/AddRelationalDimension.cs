using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class AddRelationalDimension : ICommand
{
    public Guid DomainModelId {get; set;}
    public Guid DomainObjectId {get;set;}    
    public string RelationName {get;set;}
    public string RelationTarget {get;set;}

    public AddRelationalDimension(Guid domainModelId,Guid domainObjectId, string relationName, string relationTarget)
    {
        DomainObjectId = domainObjectId;
        RelationName = relationName;
        RelationTarget = relationTarget;
        DomainModelId = domainModelId;
    }
}
