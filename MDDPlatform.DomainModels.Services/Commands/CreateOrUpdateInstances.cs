using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.DomainModels.Services.Commands.Common;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands;
public class CreateOrUpdateInstances : BaseRequest
{
    public Guid DomainModelId {get;set;}
    public List<InstanceProperties> Instances {get;set;}

    public CreateOrUpdateInstances(Guid domainModelId, List<InstanceProperties> instances)
    {
        DomainModelId = domainModelId;
        Instances = instances;
        CoordinationId = Guid.Empty;
        StepId = Guid.Empty;
    }
    public CreateOrUpdateInstances(Guid domainModelId, List<InstanceProperties> instances,Guid coordinationId,Guid stepId)
    {
        DomainModelId = domainModelId;
        Instances = instances;
        CoordinationId = coordinationId;
        StepId = stepId;
    }
    public CreateOrUpdateInstances()
    {
        DomainModelId = Guid.Empty;
        Instances = new();
        CoordinationId = Guid.Empty;
        StepId = Guid.Empty;        
    }    

}

public class InstanceProperties
{
    public string InstanceName {get;set;}
    public string InstanceType {get;set;}
    public List<DomainObjectProperty> PropertyValues {get;set;}

    public InstanceProperties(string instanceName, string instanceType, List<DomainObjectProperty> propertyValues)
    {
        InstanceName = instanceName;
        InstanceType = instanceType;
        PropertyValues = propertyValues;
    }
}

public class CreateOrUpdateInstancesHandler : ICommandHandler<CreateOrUpdateInstances>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public CreateOrUpdateInstancesHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(CreateOrUpdateInstances command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(CreateOrUpdateInstances command)
    {
        List<IDomainEvent> events = new();
        var domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        if(Equals(domainModel,null))
            throw new Exception("Domain Model Not Found");
        
        foreach(var instance in command.Instances){
            domainModel.TryCreateDomainObject(instance.InstanceType,instance.InstanceName);
            foreach(var item in instance.PropertyValues){
                domainModel.TrySetDomainObjectProperty(instance.InstanceType,instance.InstanceName,item.Name,item.Value);
            }
            events.AddRange(domainModel.DomainEvents.ToList());
            domainModel.ClearEvents();
        }
        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
    }
}
