using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Services.Exceptions;
using MDDPlatform.DomainModels.Services.Intefaces;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers.V2;
public class RemoveDomainObjectHandler : ICommandHandler<RemoveDomainObject>
{
    private IDomainModelRepository _domainModdelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;
    
    private IDomainModelNotificationService _notificationService;


    public RemoveDomainObjectHandler(IDomainModelRepository domainModdelRepository, IDomainModelNotificationService notificationService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModdelRepository = domainModdelRepository;
        _notificationService = notificationService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(RemoveDomainObject command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(RemoveDomainObject command)
    {
        DomainModel domainModel = await _domainModdelRepository.GetDomainModelAsync(command.DomainModelId);
        domainModel.RemoveDomainObject(command.DomainObjectId);
        await _domainModdelRepository.UpdateDomainModelAsync(domainModel);

        List<IDomainEvent> events = domainModel.DomainEvents.ToList();
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
        domainModel.ClearEvents();

        foreach(var @event in events)
        {
            if(@event.GetType() == typeof(DomainModelUpdated))
                await _notificationService.DomainModelUpdatedAsync((DomainModelUpdated) @event);                           
        }

    }
}
