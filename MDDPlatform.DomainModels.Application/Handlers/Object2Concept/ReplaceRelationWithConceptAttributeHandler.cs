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
public class ReplaceRelationWithConceptAttributeHandler : ICommandHandler<ReplaceRelationWithConceptAttribute>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationWithConceptAttributeHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationWithConceptAttribute command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationWithConceptAttribute command)
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

    private async Task HandleModelOperationAsync(ReplaceRelationWithConceptAttribute command)
    {
        Dictionary<string,string> keyValues = new();        
        List<string> concepts = new();

        var tuples = await _domainModelService.GetRelatedObjectsAsync(command.InputModel,command.SourceNode,command.SourceToDestinationRelation,command.DestinationNode);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        foreach(var tuple in tuples)
        {
            var sourceNodeInstance = tuple.Item1;
            var destinationNodeInstance = tuple.Item3;
            
            string conceptFullName = sourceNodeInstance.FullName.Value;
            if(!concepts.Contains(conceptFullName))
            {
                outputDomainModel.TryCreateDomainConcept(sourceNodeInstance.Name, sourceNodeInstance.Type);
                concepts.Add(conceptFullName);
            }
            keyValues = destinationNodeInstance.ToKeyValueExpressionResolver("DestinationNode");

            var attributeName = command.AttributeNameExpression.ResolveExpression(keyValues);
            var attributeValue = command.AttributeValueExpression.ResolveExpression(keyValues);

            outputDomainModel.TryAppendAttributeValue(sourceNodeInstance.Name,sourceNodeInstance.Type,attributeName,attributeValue);
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
