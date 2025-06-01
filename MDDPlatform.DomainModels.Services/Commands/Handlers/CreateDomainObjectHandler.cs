using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Services.Intefaces;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers.V2;
public class CreateDomainObjectHandler : ICommandHandler<CreateDomainObject>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;
    private readonly IDomainModelNotificationService _notificationService;

    public CreateDomainObjectHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper, IDomainModelNotificationService notificationService)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
        _notificationService = notificationService;
    }

    public void Handle(CreateDomainObject command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(CreateDomainObject command)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        domainModel.CreateDomainObject(command.DomainConceptId,command.InstanceName,command.DomainObjectProperties,command.DomainObjectRelations,command.RelationalDimensions);
        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        List<IDomainEvent> events = domainModel.DomainEvents.ToList();
        await _messageBroker.PublishAsync(_eventMapper.Map(events));

        foreach(var @event in events)
        {
            if(@event.GetType() == typeof(DomainModelUpdated))
                await _notificationService.DomainModelUpdatedAsync((DomainModelUpdated) @event);                           
        }
        domainModel.ClearEvents();
    }
}
