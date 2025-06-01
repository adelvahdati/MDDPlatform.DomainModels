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
public class ReplaceRelationWithPropertyHandler : ICommandHandler<ReplaceRelationWithProperty>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationWithPropertyHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationWithProperty command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationWithProperty command)
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

    private async Task HandleModelOperationAsync(ReplaceRelationWithProperty command)
    {
        var tuples = await _domainModelService.GetRelatedObjectsAsync(command.InputModel, command.SourceNode, command.SourceToDestinationRelation);
        if(tuples.Count == 0)
        {
            await HandlePartialModelTransformationAsync(command);
            return;
        }
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        List<string> concepts = new();
        foreach (var tuple in tuples)
        {
            var Concept = tuple.Item1;
            var Property = tuple.Item3;
            string conceptFullName = Concept.FullName.Value;
            if (!concepts.Contains(conceptFullName))
            {
                outputDomainModel.TryCreateDomainConcept(Concept.Name, Concept.Type);
                concepts.Add(conceptFullName);
            }
            outputDomainModel.AddProperty(Concept.Name, Concept.Type, Property.Name, Property.Type);
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }

    private async Task HandlePartialModelTransformationAsync(ReplaceRelationWithProperty command)
    {
        DomainModel inputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.InputModel);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        DomainConcept? domainConcept = inputDomainModel.GetDomainConcept(ConceptFullName.Create(command.SourceNode));
        if (!Equals(domainConcept, null))
        {
            var instances = domainConcept.Instances;
            foreach (var instance in instances)
            {
                var targetInstances = instance.GetTargetInstances(command.SourceToDestinationRelation);
                outputDomainModel.TryCreateDomainConcept(instance.Name, instance.Type);
                foreach (var targetInstance in targetInstances)
                {
                    outputDomainModel.AddProperty(instance.Name, instance.Type, targetInstance.Name, targetInstance.Type);
                }
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
