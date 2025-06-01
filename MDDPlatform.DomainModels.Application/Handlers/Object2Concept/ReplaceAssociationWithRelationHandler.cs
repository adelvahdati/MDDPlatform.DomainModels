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
public class ReplaceAssociationWithRelationHandler : ICommandHandler<ReplaceAssociationWithRelation>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceAssociationWithRelationHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceAssociationWithRelation command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceAssociationWithRelation command)
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

    private async Task HandleModelOperationAsync(ReplaceAssociationWithRelation command)
    {
        var tuples = await _domainModelService.GetRelatedObjectsAsync(command.InputModel,command.SourceNode,command.SourceToDestinationRelation,command.DestinationNode);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        List<string> concepts = new();
        foreach(var tuple in tuples)
        {
            var concept = tuple.Item1;
            var association = tuple.Item3;
            string conceptFullName = concept.FullName.Value;
            if (!concepts.Contains(conceptFullName))
            {
                outputDomainModel.TryCreateDomainConcept(concept.Name, concept.Type);
                concepts.Add(conceptFullName);
            }
            var relationName = association.GetPropertyValue(command.RelationNameProperty);
            var relationTarget = association.GetPropertyValue(command.RelationTargetProperty);
            var multiplicity = association.GetPropertyValue(command.MultiplicityProperty);
            if(!string.IsNullOrEmpty(relationName) && !string.IsNullOrEmpty(relationTarget) && !string.IsNullOrEmpty(multiplicity)){
                outputDomainModel.AddRelation(concept.Name,concept.Type,relationName,relationTarget,multiplicity);
            }
            
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
