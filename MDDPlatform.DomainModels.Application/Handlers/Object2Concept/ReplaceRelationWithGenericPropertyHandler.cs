using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class ReplaceRelationWithGenericPropertyHandler : ICommandHandler<ReplaceRelationWithGenericProperty>
{
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReplaceRelationWithGenericPropertyHandler(IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ReplaceRelationWithGenericProperty command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReplaceRelationWithGenericProperty command)
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

    private Task HandleModelOperationAsync(ReplaceRelationWithGenericProperty command)
    {
        throw new NotImplementedException();
    }
}
