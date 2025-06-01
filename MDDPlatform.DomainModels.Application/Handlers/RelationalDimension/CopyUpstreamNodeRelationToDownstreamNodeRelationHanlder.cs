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
public class CopyUpstreamNodeRelationToDownstreamNodeRelationHandler : ICommandHandler<CopyUpstreamNodeRelationToDownstreamNodeRelation>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public CopyUpstreamNodeRelationToDownstreamNodeRelationHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(CopyUpstreamNodeRelationToDownstreamNodeRelation command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(CopyUpstreamNodeRelationToDownstreamNodeRelation command)
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

    private async Task HandleModelOperationAsync(CopyUpstreamNodeRelationToDownstreamNodeRelation command)
    {
        DomainModel DownStreamModel = await _domainModelRepository.GetDomainModelAsync(command.DownStreamModel);
        var sourceNodeInstances = DownStreamModel.GetDomainObjects(command.SourceNode);

        DomainModel UpStreamModel = await _domainModelRepository.GetDomainModelAsync(command.UpStreamModel);
        var destinationConcepts = UpStreamModel.GetDomainConceptByType(command.DestinationNode);
        foreach (var instance in sourceNodeInstances)
        {
            var upStreamTargetInstances = instance.GetRelationalTargetInstances(command.SourceToDestinationRelationalDimension);
            foreach (var upStreamTargetInstance in upStreamTargetInstances)
            {
                var destinationConcept = destinationConcepts.Where(d => d.FullName.Value.ToLower() == upStreamTargetInstance.ToLower()).FirstOrDefault();
                if (!Equals(destinationConcept, null))
                {
                    var relationTargets = destinationConcept.GetRelationTarget(command.UpStreamNodeRelation);
                    foreach(var relationTarget in relationTargets){
                        var targetFullName = ConceptFullName.Create(relationTarget.Value);
                        var targetName = targetFullName.ExtractConceptName();
                        var targetType = targetFullName.ExtractConceptType();
                        DownStreamModel.TryCreateDomainObject(targetType,targetName);
                        DownStreamModel.TrySetRelationTargetInstance(instance.Id,command.DownStreamNodeRelation,targetType,targetName);
                    }
                }
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(DownStreamModel);
    }
}
