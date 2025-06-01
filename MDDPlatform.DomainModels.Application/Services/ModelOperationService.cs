using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Services.Commands.Common;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Dispatchers;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Services;
public class ModelOperationService : IModelOperationService
{
    private readonly IMessageDispatcher _messageDispatcher;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;


    public ModelOperationService(IMessageDispatcher messageDispatcher, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _messageDispatcher = messageDispatcher;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public async Task HandleAsync(BaseRequest request)
    {
        IDomainEvent @event;
        try
        {
            await _messageDispatcher.HandleAsync(request);
            @event = new ModelOperationCompleted(request.CoordinationId,request.StepId,request.GetType().Name);
            await _messageBroker.PublishAsync(_eventMapper.Map(@event));

        }catch(Exception ex)
        {
            @event = new ModelOperationFailed(request.CoordinationId,request.StepId,request.GetType().Name,ex.Message);
            await _messageBroker.PublishAsync(_eventMapper.Map(@event));

        }
    }
}
        
