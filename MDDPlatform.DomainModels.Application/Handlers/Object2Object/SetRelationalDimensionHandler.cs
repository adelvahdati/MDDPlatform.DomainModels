using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;

public class SetRelationalDimensionHandler : ICommandHandler<SetRelationalDimension>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public SetRelationalDimensionHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(SetRelationalDimension command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(SetRelationalDimension command)
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

    private async Task HandleModelOperationAsync(SetRelationalDimension command)
    {
        Dictionary<string,string> keyValues;
        var inputModel = await _domainModelRepository.GetDomainModelAsync(command.InputModel);
        var domainObjects = await _domainModelService.GetDomainObjectsByTypeAsync(command.InputModel,command.Element);
        foreach(var domainObject in domainObjects)
        {
            keyValues = new();
            keyValues = domainObject.ToKeyValueExpressionResolver("Element");
            var relationName = command.RelationNameExpression.ResolveExpression(keyValues);
            var relationTarget = command.RelationTargetExpression.ResolveExpression(keyValues);
            inputModel.TryAddRelationalDimension(domainObject.InstanceType,domainObject.InstanceName, relationName,relationTarget);            
        }
        await _domainModelRepository.UpdateDomainModelAsync(inputModel);
    }
}
