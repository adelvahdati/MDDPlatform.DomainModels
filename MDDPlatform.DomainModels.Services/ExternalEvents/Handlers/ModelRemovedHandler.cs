

using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.ExternalEvents.Handlers;
public class ModelRemovedHandler : IEventHandler<ModelRemoved>
{
    private IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ModelRemovedHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ModelRemoved @event)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ModelRemoved @event)
    {      
        var result = await _domainModelRepository.DeleteDomainModelAsync(@event.DomainId,@event.ModelId);
        if(result)
            await _messageBroker.PublishAsync(_eventMapper.Map(new DomainModelRemoved(@event.DomainId,@event.ModelId)));
    }
}
