using System.Runtime.CompilerServices;
using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class SetHigherDimensionRelationHandler : ICommandHandler<SetHigherDimensionRelation>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public SetHigherDimensionRelationHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(SetHigherDimensionRelation command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(SetHigherDimensionRelation command)
    {
        IDomainEvent @event;
        try
        {
            await HandleModelOperationAsync(command);            
            @event = new ModelOperationCompleted(command.CoordinationId,command.StepId,command.GetType().Name);

        }catch(Exception ex)
        {
            @event = new ModelOperationFailed(command.CoordinationId,command.StepId,command.GetType().Name,ex.Message);
        }
        await _messageBroker.PublishAsync(_eventMapper.Map(@event));
    }

    private async Task HandleModelOperationAsync(SetHigherDimensionRelation command)
    {
        if(string.IsNullOrEmpty(command.UpstreamConcept))
            throw new Exception("Upstream concept Should not be null or empty");

        var upstreamConcepts = command.UpstreamConcept
                                        .Split(",")
                                        .Select(item=> item.Trim())
                                        .Where(item=>!string.IsNullOrEmpty(item))
                                        .ToList();
        
        if(upstreamConcepts==null)
            throw new Exception("Upstream concept Should not be null");

        if(upstreamConcepts.Count == 0)
            throw new Exception("Upstream concept Should not be empty");

        var upStreamObjects = await _domainModelService.GetDomainObjectsOfTypesAsync(command.UpstreamModel,upstreamConcepts);

        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.DownstreamModel);

        Dictionary<string,string> keyValues;

        foreach(var domainObject in upStreamObjects)
        {
            keyValues=new();
            keyValues = domainObject.ToKeyValueExpressionResolver("UpstreamConcept");

            var relationName = command.RelationNameExpression.ResolveExpression(keyValues);
            var relationTarget = command.RelationTargetExpression.ResolveExpression(keyValues);

            outputModel.TryAddRelationalDimension(command.DownstreamConcept,domainObject.InstanceName,relationName,relationTarget);
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }
}
