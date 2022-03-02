using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.ActionResults;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers
{
    public class CreateConceptHandler : ICommandHandler<CreateConcept>
    {
        private readonly IDomainModelRepository _domainModelRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;


        public CreateConceptHandler(IDomainModelRepository domainModelRepository,IMessageBroker messageBroker, IEventMapper eventMapper)
        {
            _domainModelRepository = domainModelRepository;
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public void Handle(CreateConcept command)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(CreateConcept command)
        {
            DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(command.ModelId);
            if(domainModel.Equals(null))
            {
                Console.WriteLine("Model is not defined");
                return;
            }
            
            var addConceptAction  = domainModel.CreateConcept(command.Name,command.Type);
            if(addConceptAction.Status == ActionStatus.Failure)
            {
                Console.WriteLine(addConceptAction.Message);
                return;
            }
            bool result = addConceptAction.Result;
            if(!result)
            {
                Console.WriteLine("Unexpected result : " + addConceptAction.Message);
                return;
            }
            await _domainModelRepository.UpdateDomainModelAsync(domainModel);
            List<IDomainEvent> events =  domainModel.DomainEvents.ToList();
            await _messageBroker.PublishAsync(_eventMapper.Map(events));
            domainModel.ClearEvents();
        }
    }
}