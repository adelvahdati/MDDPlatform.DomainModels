using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class SetRelationTargetInstance : ActionResolver
{
    public SetRelationTargetInstance() : base(nameof(SetRelationTargetInstance), 5)
    {
    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        var domainObjectType = args[0];
        var domainObjectName = args[1];
        var relationName = args[2];
        var relationTarget = args[3];
        var targetInstance = args[4];

        // var instanceTypeFullname = domainModel.GetTypeFullName(domainObjectType);
        // var relationTargetFullName = domainModel.GetTypeFullName(relationTarget);

        // domainModel.TrySetRelationTargetInstance(instanceTypeFullname,domainObjectName,relationName,relationTargetFullName,targetInstance);        

        domainModel.TrySetRelationTargetInstance(domainObjectType,domainObjectName,relationName,relationTarget,targetInstance);        

    }
}