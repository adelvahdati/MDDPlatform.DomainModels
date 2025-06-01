using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class ReplaceRelationWithChainOfNodesHandler : ICommandHandler<ReplaceRelationWithChainOfNodes>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationWithChainOfNodesHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationWithChainOfNodes command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationWithChainOfNodes command)
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

    private async Task HandleModelOperationAsync(ReplaceRelationWithChainOfNodes command)
    {
        Dictionary<string,string> keyValues;

        var tuples = await _domainModelService.GetRelatedObjectsAsync(command.InputModel,command.SourceNode,command.SourceToDestinationRelation,command.DestinationNode);
        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        foreach (var tuple in tuples)
        {
            var sourceNodeObject = tuple.Item1;
            var destinationNodeObject = tuple.Item3;

            keyValues = new();
            keyValues.Add("SourceNode.Name",sourceNodeObject.Name);
            keyValues.Add("DestinationNode.Name", destinationNodeObject.Name);

            var firstNode = command.FirstNodeInstanceExpression.ResolveExpression(keyValues);
            var middleNode = command.MiddleNodeInstanceExpression.ResolveExpression(keyValues);
            var lastNode = command.LastNodeInstanceExpression.ResolveExpression(keyValues);

            outputModel.TryCreateDomainObject(command.FirstNode,firstNode);
            outputModel.TryCreateDomainObject(command.MiddleNode,middleNode);
            outputModel.TryCreateDomainObject(command.LastNode,lastNode);

            outputModel.TrySetRelationTargetInstance(command.FirstNode,firstNode,command.FirstToMiddleNodeRelation,command.MiddleNode,middleNode);
            outputModel.TrySetRelationTargetInstance(command.MiddleNode,middleNode,command.MiddleToLastNodeRelation,command.LastNode,lastNode);
        }

        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }

    
}
