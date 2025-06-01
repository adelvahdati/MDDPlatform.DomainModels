using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class SetProperty : ActionResolver
{
    public SetProperty() : base(nameof(SetProperty), 4)
    {
    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        var instanceType = args[0];
        var instanceName = args[1];
        var propertyName = args[2];
        var propertyValue = args[3];

        // var instanceTypeFullname = domainModel.GetTypeFullName(instanceType);
        // domainModel.TrySetDomainObjectProperty(instanceTypeFullname,instanceName,propertyName,propertyValue);
        
        domainModel.TrySetDomainObjectProperty(instanceType,instanceName,propertyName,propertyValue);
    }
}
