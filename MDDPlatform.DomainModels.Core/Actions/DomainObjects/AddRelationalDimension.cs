using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class AddRelationalDimension : ActionResolver
{
    public AddRelationalDimension() : base(nameof(AddRelationalDimension),4)
    {
    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        var domainObjectType = args[0];
        var domainObjectName = args[1];
        var relationName = args[2];
        var relationTarget = args[3];

        // var instanceTypeFullname = domainModel.GetTypeFullName(domainObjectType);
        // domainModel.TryAddRelationalDimension(instanceTypeFullname,domainObjectName,relationName,relationTarget);

        domainModel.TryAddRelationalDimension(domainObjectType,domainObjectName,relationName,relationTarget);

    }
}
