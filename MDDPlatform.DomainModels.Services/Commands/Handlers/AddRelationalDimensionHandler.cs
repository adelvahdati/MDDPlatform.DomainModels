using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers;
public class AddRelationalDimensionHandler : ICommandHandler<AddRelationalDimension>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public AddRelationalDimensionHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(AddRelationalDimension command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(AddRelationalDimension command)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        domainModel.AddRelationalDimension(command.DomainObjectId,command.RelationName,command.RelationTarget);
        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        List<IDomainEvent> events = domainModel.DomainEvents.ToList();
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
        domainModel.ClearEvents();                
    }
}
