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


        public ModelCreatedHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
        {
            _domainModelRepository = domainModelRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public void Handle(ModelCreated @event)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(ModelCreated @event)
        {
            var domainModelCreation = DomainModel.Create(@event.ModelId,@event.Name,@event.Tag,@event.Type,@event.Level,new Domain(@event.DomainId));
            if(domainModelCreation.Status == SharedKernel.ActionResults.ActionStatus.Failure)
            {
                Console.WriteLine(domainModelCreation.Message);
                return;
            }
            DomainModel? domainModel = domainModelCreation.Result;
            if(Equals(domainModel,null))
            {
                Console.WriteLine("Unexpected result in Domain model creation");
                return;
            }
            await _domainModelRepository.CreateDomainModelAsync(domainModel);
            await _messageBroker.PublishAsync(_eventMapper.Map(domainModel.DomainEvents.ToList()));
            domainModel.ClearEvents();            
        }
    }
}