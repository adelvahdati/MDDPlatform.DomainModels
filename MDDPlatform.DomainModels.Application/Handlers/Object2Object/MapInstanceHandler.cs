using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MapInstanceHandler : ICommandHandler<MapInstance>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MapInstanceHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MapInstance command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MapInstance command)
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

    private async Task HandleModelOperationAsync(MapInstance command)
    {
        var inputModel = await _domainModelRepository.GetDomainModelAsync(command.InputModel);
        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        List<string> objectTypes = inputModel.GetObjectTypes();
        foreach(var objectType in  objectTypes)
        {
            if(outputModel.SupportObjectType(objectType))
            {
                var objectDtos = await _domainModelService.GetDomainObjectsByTypeAsync(command.InputModel,objectType);
                foreach(var objectDto in objectDtos)
                {
                    outputModel.TryCreateDomainObject(objectDto.InstanceType,objectDto.InstanceName);
                }
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }
}
