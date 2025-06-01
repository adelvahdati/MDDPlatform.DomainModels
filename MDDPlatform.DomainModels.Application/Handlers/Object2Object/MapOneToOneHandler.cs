using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MapOneToOneHandler : ICommandHandler<MapOneToOne>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MapOneToOneHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MapOneToOne command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MapOneToOne command)
    {
        IDomainEvent @event;
        try
        {
            await HandleModelOperationAsync(command);            
            @event = new ModelOperationCompleted(command.CoordinationId,command.StepId,command.GetType().Name);
        }
        catch(Exception ex)
        {
            @event = new ModelOperationFailed(command.CoordinationId,command.StepId,command.GetType().Name,ex.Message);
        }
        await _messageBroker.PublishAsync(_eventMapper.Map(@event));
    }
    private async Task HandleModelOperationAsync(MapOneToOne command)
    {
        var inputModel = await _domainModelRepository.GetDomainModelAsync(command.InputModel);
        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        
        var sourceObjects = await _domainModelService.GetDomainObjectsByTypeAsync(command.InputModel,command.Source);
        foreach(var sourceObject in sourceObjects)
        {
            outputModel.TryCreateDomainObject(command.Destination,sourceObject.InstanceName);

        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }

}
