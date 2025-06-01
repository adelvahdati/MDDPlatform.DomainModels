using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class ExtractOperationAttributeFromChainOfNodesHandler : ICommandHandler<ExtractOperationAttributeFromChainOfNodes>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ExtractOperationAttributeFromChainOfNodesHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ExtractOperationAttributeFromChainOfNodes command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ExtractOperationAttributeFromChainOfNodes command)
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

    private async Task HandleModelOperationAsync(ExtractOperationAttributeFromChainOfNodes command)
    {
        var tuples = await _domainModelService.GetChainOfRelatedObjectAsync(command.InputModel,command.FirstNode,command.FirstToMiddleNodeRelation,command.MiddleNode,command.MiddleToLastNodeRelation,command.LastNode);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        Dictionary<string,string> keyValues;
        List<string> concepts = new();

        foreach(var tuple in tuples){
            var firstNode = tuple.Item1;
            var middlenode = tuple.Item3;
            var lastNode = tuple.Item5;

            keyValues = new();
            keyValues = firstNode.ToKeyValueExpressionResolver("FirstNode")
                                    .AppendKeyValueExpressionResolver(middlenode,"MiddleNode")
                                    .AppendKeyValueExpressionResolver(lastNode,"LastNode");

            var conceptName = command.ConceptNameExpression.ResolveExpression(keyValues);
            var conceptType = command.ConceptTypeExpression.ResolveExpression(keyValues);

            outputDomainModel.TryCreateDomainConcept(conceptName,conceptType);

            var operationName = command.OperationNameExpression.ResolveExpression(keyValues);
            var attributeName = command.AttributeNameExpression.ResolveExpression(keyValues);
            var attributeValue = command.AttributeValueExpression.ResolveExpression(keyValues);
            outputDomainModel.TrySetOperationAttribute(conceptName,conceptType,operationName,attributeName,attributeValue);
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
