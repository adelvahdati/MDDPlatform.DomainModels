using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.DomainModels.Services.Commands.Common;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands;
public class CreateOrUpdateDomainObjectProperties : BaseRequest
{
    public Guid DomainModelId {get;set;}
    public string InstanceName {get;set;}
    public string InstanceType {get;set;}
    public List<DomainObjectProperty> PropertyValues {get;set;}

    public CreateOrUpdateDomainObjectProperties(Guid domainModelId, string instanceName, string instanceType, List<DomainObjectProperty> propertyValues)
    {
        DomainModelId = domainModelId;
        InstanceName = instanceName;
        InstanceType = instanceType;
        PropertyValues = propertyValues;
    }
}
public class CreateOrUpdateDomainObjectPropertiesHandler : ICommandHandler<CreateOrUpdateDomainObjectProperties>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public CreateOrUpdateDomainObjectPropertiesHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(CreateOrUpdateDomainObjectProperties command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(CreateOrUpdateDomainObjectProperties command)
    {
        var domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        domainModel.TryCreateDomainObject(command.InstanceType,command.InstanceName);
        foreach(var item in command.PropertyValues){
                    domainModel.TrySetDomainObjectProperty(command.InstanceType,command.InstanceName,item.Name,item.Value);
        }

        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        List<IDomainEvent> events = domainModel.DomainEvents.ToList();
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
        domainModel.ClearEvents();

    }
}