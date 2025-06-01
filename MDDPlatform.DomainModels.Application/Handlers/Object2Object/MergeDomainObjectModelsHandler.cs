using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MergerDomainObjectModelsHandler : ICommandHandler<MergerDomainObjectModels>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MergerDomainObjectModelsHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MergerDomainObjectModels command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MergerDomainObjectModels command)
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

    private async Task HandleModelOperationAsync(MergerDomainObjectModels command)
    {
        if(command.FirstModel == command.OutputModel)
            await Merge(command.SecondModel,command.FirstModel);
        else if(command.SecondModel == command.OutputModel)
            await Merge(command.FirstModel,command.SecondModel);
        else 
            await Merge(command.FirstModel,command.SecondModel,command.OutputModel);
    }

    private async Task Merge(Guid inputModelId,Guid outputModelId)
    {
        var sourceModel = await _domainModelRepository.GetDomainModelAsync(inputModelId);
        var destinationModel = await _domainModelRepository.GetDomainModelAsync(outputModelId);
        var soucreDomainObjects = sourceModel.GetDomainObjects();
        foreach(var domainObject in soucreDomainObjects){            
            destinationModel.TryCloneObject(domainObject);            
        }
        await _domainModelRepository.UpdateDomainModelAsync(destinationModel);

    }
    private async Task Merge(Guid inputModel1,Guid inputModel2,Guid outputModel){
        var sourceModel1 = await _domainModelRepository.GetDomainModelAsync(inputModel1);
        var sourceModel2 = await _domainModelRepository.GetDomainModelAsync(inputModel2);

        var destinationModel = await _domainModelRepository.GetDomainModelAsync(outputModel);

        var soucreDomainObjects = sourceModel1.GetDomainObjects();
        foreach(var domainObject in soucreDomainObjects){            
            destinationModel.TryCloneObject(domainObject);            
        }

        soucreDomainObjects = sourceModel2.GetDomainObjects();
        foreach(var domainObject in soucreDomainObjects){            
            destinationModel.TryCloneObject(domainObject);            
        }

        await _domainModelRepository.UpdateDomainModelAsync(destinationModel);

    }
}
