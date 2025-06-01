using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Services.Intefaces;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers;
public class UpdateDomainObjectHandler : ICommandHandler<UpdateDomainObject>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelNotificationService _notificationService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public UpdateDomainObjectHandler(IDomainModelRepository domainModelRepository, IDomainModelNotificationService notificationService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _notificationService = notificationService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(UpdateDomainObject command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(UpdateDomainObject command)
    {
        DomainModel? domainModel =  await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        domainModel.UpdateDomainObject(command.DomainObjectId,command.InstanceName,command.Properties,command.Relations,command.RelationalDimensions);

        await _domainModelRepository.UpdateDomainModelAsync(domainModel);

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
