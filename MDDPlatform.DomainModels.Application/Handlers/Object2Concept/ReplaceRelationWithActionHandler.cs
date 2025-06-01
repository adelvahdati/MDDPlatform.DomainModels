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
public class ReplaceRelationWithActionHandler : ICommandHandler<ReplaceRelationWithAction>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationWithActionHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationWithAction command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationWithAction command)
    {
        IDomainEvent @event;
        try
        {
            await HandlePartialModelTransformationAsync(command);
            @event = new ModelOperationCompleted(command.CoordinationId,command.StepId,command.GetType().Name);
        }
        catch(Exception ex)
        {
            @event = new ModelOperationFailed(command.CoordinationId,command.StepId,command.GetType().Name,ex.Message);
        }
        await _messageBroker.PublishAsync(_eventMapper.Map(@event));
    }

    private async Task HandlePartialModelTransformationAsync(ReplaceRelationWithAction command)
    {
        string operationName, operationOutput;
        Dictionary<string,string> keyValues = new();
        DomainModel inputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.InputModel);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        DomainConcept? domainConcept = inputDomainModel.GetDomainConcept(ConceptFullName.Create(command.ConceptNode));
        if (!Equals(domainConcept, null))
        {
            var instances = domainConcept.Instances;
            foreach (var instance in instances)
            {
                keyValues = new();
                keyValues.Add("ConceptNode.Name",instance.Name);

                var targetInstances = instance.GetTargetInstances(command.OperationInputsRelation);
                outputDomainModel.TryCreateDomainConcept(instance.Name, instance.Type);

                operationOutput = command.OperationOutputProperty;
                operationName = command.OperationNameProperty.ResolveExpression(keyValues);

                var OpName = OperationName.Create(operationName);
                var OpOutput = OperationOutput.Create(operationOutput, false);
                
                var OpInputs = new List<OperationInput>();
                if (targetInstances.Count > 0)
                    OpInputs = targetInstances.Select(targetInstance => OperationInput.Create(targetInstance.Name.ToLower(), targetInstance.FullName.Value))
                                        .ToList();
                
                outputDomainModel.AddOperation(instance.Name, instance.Type, OpName, OpInputs, OpOutput);
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }

    private async Task HandleModelTransformationAsync(ReplaceRelationWithAction command)
    {
        Dictionary<string,string> keyValues = new();        
        string operationName, operationOutput;
        List<string> concepts = new();

        var tuples = await _domainModelService.GetRelatedObjectsListAsync(command.InputModel, command.ConceptNode, command.OperationInputsRelation);

        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        foreach (var tuple in tuples)
        {
            var concept = tuple.Item1;

            keyValues = new();
            keyValues.Add("ConceptNode.Name",concept.Name);

            string conceptFullName = concept.FullName.Value;
            if (!concepts.Contains(conceptFullName))
            {
                outputDomainModel.TryCreateDomainConcept(concept.Name, concept.Type);
                concepts.Add(conceptFullName);
            }
            operationOutput = command.OperationOutputProperty;            
            operationName = command.OperationNameProperty.ResolveExpression(keyValues);

            var inputs = tuple.Item3;
            if (!string.IsNullOrEmpty(operationName))
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
