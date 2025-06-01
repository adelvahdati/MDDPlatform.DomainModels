using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class CreateDomainConceptInstance : ICommand
{
    public Guid DomainModelId {get;set;}
    public string InstanceType {get;set;}
    public string InstanceName {get;set;}
    public Guid InstanceId {get;set;}

    public CreateDomainConceptInstance(Guid domainModelId, string instanceType, string instanceName, Guid instanceId)
    {
        this.DomainModelId = domainModelId;
        this.InstanceType = instanceType;
        this.InstanceName = instanceName;
        this.InstanceId = instanceId;
    }
}