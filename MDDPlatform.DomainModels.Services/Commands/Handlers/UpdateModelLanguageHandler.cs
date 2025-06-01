using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers;
public class UpdateModelLanguageHandler : ICommandHandler<UpdateModelLanguage>
{
        private IDomainModelRepository _domainModelRepository;
        private readonly ILanguageService _languageService;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

    public UpdateModelLanguageHandler(IDomainModelRepository domainModelRepository, ILanguageService languageService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _languageService = languageService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(UpdateModelLanguage command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(UpdateModelLanguage command)
    {
        List<BaseConcept> baseConcepts = await _languageService.GetLanguageElements(command.LanguageId);
        DomainModel? domainModel = await _domainModelRepository.GetDomainModelAsync(command.ModelId);
        if(Equals(domainModel,null))
            throw new Exception("Model not found");
        
        domainModel.UpdateModelLanguage(baseConcepts);
        await _domainModelRepository.UpdateDomainModelAsync(domainModel);        
        List<IDomainEvent> events = new List<IDomainEvent>() { new ModelLanguageUpdated(command.ModelId,command.LanguageId)};
        await _messageBroker.PublishAsync(_eventMapper.Map(events));
        domainModel.ClearEvents();            
    }
}
