using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class CreateInstance : ActionResolver
{
    public CreateInstance() : base(nameof(CreateInstance), 2)
    {
    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        var instanceType = args[0];
        var instanceName = args[1];

        // var instanceTypeFullname = domainModel.GetTypeFullName(instanceType);
        // domainModel.TryCreateDomainObject(instanceTypeFullname,instanceName);        

        domainModel.TryCreateDomainObject(instanceType,instanceName);        
    }
}
