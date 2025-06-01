using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands;

public class SetRelationTargetInstances : ICommand
{
    public Guid DomainModelId {get;set;}
    public string SourceNodeName {get;set;}
    public string SourceNodeType {get;set;}
    public string RelationName {get;set;}
    public string RelationTarget {get;set;}
    public List<string> TargetInstances {get;set;}

    public SetRelationTargetInstances(Guid domainModelId, string sourceNodeName, string sourceNodeType, string relationName, string relationTarget, List<string> targetInstances)
    {
        DomainModelId = domainModelId;
        SourceNodeName = sourceNodeName;
        SourceNodeType = sourceNodeType;
        RelationName = relationName;
        RelationTarget = relationTarget;
        TargetInstances = targetInstances;
    }
}
public class SetRelationTargetInstancesHandler : ICommandHandler<SetRelationTargetInstances>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public SetRelationTargetInstancesHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(SetRelationTargetInstances command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(SetRelationTargetInstances command)
    {
        var domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        var targetInstances = string.Join(",",command.TargetInstances);
        domainModel.TrySetRelationTargetInstance(command.SourceNodeType,command.SourceNodeName,command.RelationName,command.RelationTarget,targetInstances);

        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        List<IDomainEvent> events = domainModel.DomainEvents.ToList();
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
        domainModel.ClearEvents();
    }
}
