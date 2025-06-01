using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers.V2;
public class SetDomainObjectPropertyHandler : ICommandHandler<SetDomainObjectProperty>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public SetDomainObjectPropertyHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(SetDomainObjectProperty command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(SetDomainObjectProperty command)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        domainModel.SetDomainObjectProperty(command.DomainObjectId,command.PropertyName,command.PropertyValue);
        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        List<IDomainEvent> events = domainModel.DomainEvents.ToList();
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
        domainModel.ClearEvents();

    }
}
