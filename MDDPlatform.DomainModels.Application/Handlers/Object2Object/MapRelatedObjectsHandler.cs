using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MapRelatedObjectsHandler : ICommandHandler<MapRelatedObjects>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MapRelatedObjectsHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MapRelatedObjects command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MapRelatedObjects command)
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

    private async Task HandleModelOperationAsync(MapRelatedObjects command)
    {

        var tuples = await _domainModelService.GetRelatedObjectsAsync(command.InputModel,command.InputSource,command.InputSourceToDestinationRelation,command.InputDestination);
        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        Dictionary<string,string> keyValues;
        
        foreach(var tuple in tuples)
        {
            var sourceObject = tuple.Item1;
            var destinationObject = tuple.Item3;
            keyValues = new Dictionary<string,string>();
            keyValues.Add("InputSource.Name",sourceObject.Name);
            keyValues.Add("InputDestination.Name",destinationObject.Name);
            var toSource = command.OutputSourceInstanceExpression.ResolveExpression(keyValues);
            var toDestination = command.OutputDestinationInstanceExpression.ResolveExpression(keyValues);

            outputModel.TryCreateDomainObject(command.OutputSource,toSource);
            outputModel.TryCreateDomainObject(command.OutputDestination,toDestination);
            outputModel.TrySetRelationTargetInstance(command.OutputSource,toSource,command.OutputSourceToDestinationRelation,command.OutputDestination,toDestination);            
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }
}
