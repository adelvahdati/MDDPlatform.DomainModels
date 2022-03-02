using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers
{
    public class RemoveConceptHandler : ICommandHandler<RemoveConcept>
    {
        private readonly IDomainModelRepository _domainModelRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;

        public RemoveConceptHandler(IDomainModelRepository domainModelRepository, 
                                            IMessageBroker messageBroker, 
                                            IEventMapper eventMapper)
        {
            _domainModelRepository = domainModelRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public void Handle(RemoveConcept command)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(RemoveConcept command)
        {
            DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(command.ModelId);
            var removeAction =  domainModel.RemoveConcept(command.Name,command.Type);
            if(removeAction.Status == SharedKernel.ActionResults.ActionStatus.Failure)
            {
                Console.WriteLine(removeAction.Message);
                return;
            }
            await _domainModelRepository.UpdateDomainModelAsync(domainModel);
            List<IDomainEvent> events =  domainModel.DomainEvents.ToList();
            await _messageBroker.PublishAsync(_eventMapper.Map(events));
            domainModel.ClearEvents();
        }
    }
}