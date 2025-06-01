using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.ExternalEvents.Handlers
{
    public class ModelCreatedHandler : IEventHandler<ModelCreated>
    {
        private IDomainModelRepository _domainModelRepository;

        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        private readonly ILanguageService _languageService;

        public ModelCreatedHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper, ILanguageService languageService)
        {
            _domainModelRepository = domainModelRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _languageService = languageService;
        }

        public void Handle(ModelCreated @event)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(ModelCreated @event)
        {
            
            DomainModel domainModel = DomainModel.Create(@event.ModelId,@event.Name,@event.Tag,@event.Type,@event.Level,new Domain(@event.DomainId));
            if(@event.LanguageId != Guid.Empty)
            {
                List<BaseConcept> baseConcepts = await _languageService.GetLanguageElements(@event.LanguageId);
                foreach(var concept in baseConcepts)
                {
                    domainModel.CreateDomainConcept(concept);
                }                    
            }
            await _domainModelRepository.CreateDomainModelAsync(domainModel);
            await _messageBroker.PublishAsync(_eventMapper.Map(domainModel.DomainEvents.ToList()));
            domainModel.ClearEvents();            
        }
    }
}