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
public class ReplaceRelationWithForkNodeHandler : ICommandHandler<ReplaceRelationWithForkNode>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationWithForkNodeHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationWithForkNode command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationWithForkNode command)
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

    private async Task HandleModelOperationAsync(ReplaceRelationWithForkNode command)
    {
        var tuples = await _domainModelService.GetRelatedObjectsAsync(command.InputModel,command.SourceNode,command.SourceToDestinationRelation,command.DestinationNode);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        DomainConcept? forkDomainConcept = outputDomainModel.GetDomainConcept(ConceptFullName.Create(command.ForkNode));
        if(Equals(forkDomainConcept ,null))
            throw new Exception($"Output model does not support {command.ForkNode}");
        
        RelationTarget? forkToSourceRelationTarget = forkDomainConcept.GetRelationTarget(command.ForkToSourceRelation)
                                                            .FirstOrDefault();
        RelationTarget? forkToDestinationRelationTarget = forkDomainConcept.GetRelationTarget(command.ForkToDestinationRelation)
                                                                            .FirstOrDefault();

        if(Equals(forkToSourceRelationTarget,null))
            throw new Exception($"{command.ForkToSourceRelation} relation is not defined in the {command.ForkNode} concept");

        if(Equals(forkToDestinationRelationTarget,null))
            throw new Exception($"{command.ForkToDestinationRelation} relation is not defined in the {command.ForkNode} concept");
        
        foreach(var tuple in tuples)
        {
            var sourceNodeObject = tuple.Item1;
            var targetNodeObject = tuple.Item3;
            
            outputDomainModel.TryCreateDomainObject(forkToSourceRelationTarget.Value,sourceNodeObject.Name);
            outputDomainModel.TryCreateDomainObject(forkToDestinationRelationTarget.Value,targetNodeObject.Name);

            var forkNodeInstanceName = command.ForkNodeInstanceName;

            if (forkNodeInstanceName.Contains("SourceNode.Name"))
                forkNodeInstanceName = forkNodeInstanceName.Replace("SourceNode.Name", sourceNodeObject.Name);

            if (forkNodeInstanceName.Contains("DestinationNode.Name"))
                forkNodeInstanceName = forkNodeInstanceName.Replace("DestinationNode.Name", targetNodeObject.Name);

            forkNodeInstanceName = string.Format("{0}{1}{2}", command.ForkNodeInstanceNamePrefix, forkNodeInstanceName, command.ForkNodeInstanceNamePostfix);
            
            outputDomainModel.TryCreateDomainObject(command.ForkNode,forkNodeInstanceName);
            outputDomainModel.SetRelationTargetInstance(
                command.ForkNode,
                forkNodeInstanceName,
                command.ForkToSourceRelation,
                forkToSourceRelationTarget.Value,
                sourceNodeObject.Name);
            outputDomainModel.SetRelationTargetInstance(
                command.ForkNode,
                forkNodeInstanceName,
                command.ForkToDestinationRelation,
                forkToDestinationRelationTarget.Value,
                targetNodeObject.Name);            
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
