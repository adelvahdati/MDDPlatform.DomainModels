using MDDPlatform.BaseConcepts.ValueObjects;
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
public class ReplaceRelationWithOperationHandler : ICommandHandler<ReplaceRelationWithOperation>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationWithOperationHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationWithOperation command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationWithOperation command)
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

    private async Task HandleModelOperationAsync(ReplaceRelationWithOperation command)
    {
        var tuples = await _domainModelService.GetChainOfRelatedObjectAsync(command.InputModel, command.ConceptNode, command.ConceptToOperationRelation, command.OperationNode, command.OperationToInputParametersRelation);

        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        List<string> concepts = new();

        foreach (var tuple in tuples)
        {
            var concept = tuple.Item1;
            string conceptFullName = concept.FullName.Value;
            if (!concepts.Contains(conceptFullName))
            {
                outputDomainModel.TryCreateDomainConcept(concept.Name, concept.Type);
                concepts.Add(conceptFullName);
            }

            var operation = tuple.Item3;
            var inputs = tuple.Item5;
            
            var keyValues = operation.ToKeyValueExpressionResolver("OperationNode");
            string? operationName = command.OperationNameProperty.ResolveExpression(keyValues);
            string? operationOutput = command.OperationOutputProperty.ResolveExpression(keyValues);

            if (!string.IsNullOrEmpty(operationName) && !string.IsNullOrEmpty(operationOutput))
            {
                var OpName = OperationName.Create(operationName);
                var OpOutput = OperationOutput.Create(operationOutput, false);
                var OpInputs = new List<OperationInput>();
                if (inputs.Count > 0)
                    OpInputs = inputs.Select(input => OperationInput.Create(input.Name.ToLower(), input.FullName.Value))
                                        .ToList();

                outputDomainModel.AddOperation(concept.Name, concept.Type, OpName, OpInputs, OpOutput);
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
