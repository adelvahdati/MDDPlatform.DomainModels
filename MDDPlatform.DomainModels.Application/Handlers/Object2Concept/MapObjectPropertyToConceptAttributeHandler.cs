using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MapObjectPropertyToConceptAttributeHandler : ICommandHandler<MapObjectPropertyToConceptAttribute>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;

    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MapObjectPropertyToConceptAttributeHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper, IDomainModelService domainModelService)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
        _domainModelService = domainModelService;
    }

    public void Handle(MapObjectPropertyToConceptAttribute command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MapObjectPropertyToConceptAttribute command)
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

    private async Task HandleModelOperationAsync(MapObjectPropertyToConceptAttribute command)
    {
        var inputInstances = await _domainModelService.GetDomainObjectsByTypeAsync(command.InputModel,command.TypeOfInstance);
        var outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        foreach(var instance in inputInstances)
        {
            outputModel.TryCreateDomainConcept(instance.InstanceName, instance.InstanceType);
            foreach(var property in instance.Properties)
            {
                if(property.Value!=null)
                {
                    outputModel.TrySetAttribute(instance.InstanceName,instance.InstanceType,property.Name,property.Value);
                }
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }
}
