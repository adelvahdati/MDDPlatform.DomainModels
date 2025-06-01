using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MapOneToTwoHandler : ICommandHandler<MapOneToTwo>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MapOneToTwoHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MapOneToTwo command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MapOneToTwo command)
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

    private async Task HandleModelOperationAsync(MapOneToTwo command)
    {
        var sourceObjects = await _domainModelService.GetDomainObjectsByTypeAsync(command.InputModel,command.Source);
        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        Dictionary<string,string> keyValues;

        foreach(var sourcObject in sourceObjects)
        {
            keyValues=new();
            keyValues = sourcObject.ToKeyValueExpressionResolver("Source");

            var firstMemberInstance = command.FirstDestinationInstanceNameExpression.ResolveExpression(keyValues);
            var firstMemberType = command.FirstDestination;

            var secondMemberInstance = command.SecondDestinationInstanceNameExpression.ResolveExpression(keyValues);
            var secondMemberType = command.SecondDestination;

            outputModel.TryCreateDomainObject(firstMemberType,firstMemberInstance);
            outputModel.TryCreateDomainObject(secondMemberType,secondMemberInstance);

            outputModel.TrySetRelationTargetInstance(firstMemberType,firstMemberInstance,command.FirstToSecondRelationName,secondMemberType,secondMemberInstance);

            foreach(var member in command.FirstDestinationMembers)
            {
                var propName = member.Name;
                var propValue = member.ValueExpression.ResolveExpression(keyValues);

                outputModel.TrySetDomainObjectProperty(firstMemberType,firstMemberInstance,propName,propValue);
            }

            foreach(var member in command.SecondDestinationMembers)
            {
                var propName = member.Name;
                var propValue = member.ValueExpression.ResolveExpression(keyValues);

                outputModel.TrySetDomainObjectProperty(secondMemberType,secondMemberInstance,propName,propValue);
            }

        }

        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }
}
