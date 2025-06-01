using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers.V2;

public class CreateDomainConceptHandler : ICommandHandler<CreateDomainConcept>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IConceptService _conceptService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public CreateDomainConceptHandler(IDomainModelRepository domainModelRepository, IConceptService conceptService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _conceptService = conceptService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(CreateDomainConcept command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(CreateDomainConcept command)
    {
        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(command.ModelId);

        BaseConcept concept = BaseConcept.Create(command.Name,command.Type,command.Properties,command.Relations,command.Operations);
        if(Equals(concept,null))
            throw new Exception("Concept not found");

        domainModel.CreateDomainConcept(concept);

        await _domainModelRepository.UpdateDomainModelAsync(domainModel);

        List<IDomainEvent> events =  domainModel.DomainEvents.ToList();
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
        domainModel.ClearEvents();
    }
}