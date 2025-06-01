using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class ConvertInstanceToTypeHandler : ICommandHandler<ConvertInstanceToType>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ConvertInstanceToTypeHandler(IMessageBroker messageBroker, IDomainModelRepository domainModelRepository, IEventMapper eventMapper)
    {
        _messageBroker = messageBroker;
        _domainModelRepository = domainModelRepository;
        _eventMapper = eventMapper;
    }

    public void Handle(ConvertInstanceToType command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ConvertInstanceToType command)
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
    private Task HandleModelOperationAsync(ConvertInstanceToType command)
    {
        throw new NotImplementedException();
    }
}