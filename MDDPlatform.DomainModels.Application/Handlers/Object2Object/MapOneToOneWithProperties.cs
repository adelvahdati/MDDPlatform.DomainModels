using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MapOneToOneWithPropertiesHandler : ICommandHandler<MapOneToOneWithProperties>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MapOneToOneWithPropertiesHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MapOneToOneWithProperties command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MapOneToOneWithProperties command)
    {
        IDomainEvent @event;
        try
        {
            await HandleModelOperationAsync(command);            
            @event = new ModelOperationCompleted(command.CoordinationId,command.StepId,command.GetType().Name);
        }
        catch(Exception ex)
        {
            @event = new ModelOperationFailed(command.CoordinationId,command.StepId,command.GetType().Name,ex.Message);
        }
        await _messageBroker.PublishAsync(_eventMapper.Map(@event));
    }
    private async Task HandleModelOperationAsync(MapOneToOneWithProperties command)
    {
        Dictionary<string,string> keyValues = new();        
        var inputModel = await _domainModelRepository.GetDomainModelAsync(command.InputModel);
        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        
        var sourceObjects = await _domainModelService.GetDomainObjectsByTypeAsync(command.InputModel,command.Source);
        foreach(var sourceObject in sourceObjects)
        {
            keyValues.Clear();
            keyValues = sourceObject.ToKeyValueExpressionResolver("Source");
            var instanceType = command.Destination;
            var instanceName = sourceObject.InstanceName;
            outputModel.TryCreateDomainObject(instanceType,instanceName);
            foreach(var member in command.DestinationMembers)
            {
                var propName = member.Name;
                var propValue = member.ValueExpression.ResolveExpression(keyValues);
                outputModel.TrySetDomainObjectProperty(instanceType,instanceName,propName,propValue);
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }

}
