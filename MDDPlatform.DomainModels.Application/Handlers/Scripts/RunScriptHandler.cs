using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class RunScriptHandler : ICommandHandler<RunScript>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public RunScriptHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(RunScript command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(RunScript command)
    {
        IDomainEvent @event;
        try
        {
            var domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
            foreach(var instruction in command.Instructions){
                domainModel.Execute(instruction);
            }
            await _domainModelRepository.UpdateDomainModelAsync(domainModel);
            @event = new ModelOperationCompleted(command.CoordinationId,command.StepId,command.GetType().Name);

        }
        catch (Exception ex)
        {
            @event = new ModelOperationFailed(command.CoordinationId,command.StepId,command.GetType().Name,ex.Message);
        }
        await _messageBroker.PublishAsync(_eventMapper.Map(@event));
    }
}
