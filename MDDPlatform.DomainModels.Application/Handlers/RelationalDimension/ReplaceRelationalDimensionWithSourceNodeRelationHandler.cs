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
public class ReplaceRelationalDimensionWithSourceNodeRelationHandler : ICommandHandler<ReplaceRelationalDimensionWithSourceNodeRelation>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationalDimensionWithSourceNodeRelationHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationalDimensionWithSourceNodeRelation command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationalDimensionWithSourceNodeRelation command)
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

    private async Task HandleModelOperationAsync(ReplaceRelationalDimensionWithSourceNodeRelation command)
    {
        DomainModel DownStreamModel = await _domainModelRepository.GetDomainModelAsync(command.DownStreamModel);
        var sourceNodeInstances = DownStreamModel.GetDomainObjects(command.SourceNode);

        DomainModel UpStreamModel = await _domainModelRepository.GetDomainModelAsync(command.UpStreamModel);
        var destinationConcepts = UpStreamModel.GetDomainConceptByType(command.DestinationNode)
                                                .Select(d => d.FullName)
                                                .ToList();

        foreach (var instace in sourceNodeInstances)
        {
            var upStreamTargetInstances = instace.GetRelationalTargetInstances(command.SourceToDestinationRelationalDimension);
            foreach (var targetInstance in upStreamTargetInstances)
            {
                var destinationConcept = destinationConcepts.Where(d => d.Value.ToLower() == targetInstance.ToLower()).FirstOrDefault();
                if (!Equals(destinationConcept, null))
                {
                    var instanceName = destinationConcept.ExtractConceptName();
                    DownStreamModel.SetRelationTargetInstance(instace.Id, command.RelationName, command.RelationTarget, instanceName);
                    DownStreamModel.TryCreateDomainObject(command.RelationTarget, instanceName);
                }
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(DownStreamModel);
    }
}
