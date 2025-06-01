using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class SetDomainObjectRelation : ICommand
{
    public Guid DomainModelId {get;set;}
        public Guid DomainObjectId {get;set;}    
        public string RelationName {get;set;}
        public string RelationTarget {get;set;}
        public string TargetInstance {get;set;}

    public SetDomainObjectRelation(Guid domainModelId,Guid domainObjectId, string relationName, string relationTarget, string targetInstance)
    {
        DomainObjectId = domainObjectId;
        RelationName = relationName;
        RelationTarget = relationTarget;
        TargetInstance = targetInstance;
        DomainModelId = domainModelId;
    }
}