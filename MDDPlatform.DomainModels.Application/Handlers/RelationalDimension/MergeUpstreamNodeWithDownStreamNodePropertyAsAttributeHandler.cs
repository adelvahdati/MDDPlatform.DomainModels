using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class MergeUpstreamNodeWithDownStreamNodePropertyAsAttributeHandler : ICommandHandler<MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MergeUpstreamNodeWithDownStreamNodePropertyAsAttributeHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute command)
    {
        throw new NotImplementedException();
    }
    public async Task HandleAsync(MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute command)
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

    private async Task HandleModelOperationAsync(MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute command)
    {
        DomainModel downStreamModel = await _domainModelRepository.GetDomainModelAsync(command.DownStreamModel);
        var downStreamNodeInstances = downStreamModel.GetDomainObjects(command.DownStreamNode);

        DomainModel upStreamModel = await _domainModelRepository.GetDomainModelAsync(command.UpStreamModel);

        DomainModel outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        foreach(var instance in downStreamNodeInstances)
        {
            outputModel.TryCreateDomainConcept(instance.Name,instance.Type);

            var upStreamTargetInstances = instance.GetRelationalTargetInstances(command.RelationalDimension);
            foreach(var upstreamTargetInstance in upStreamTargetInstances)
            {
                var upstreamConcept = upStreamModel.GetDomainConcept(ConceptFullName.Create(upstreamTargetInstance));
                if(!Equals(upstreamConcept,null))
                {
                    CopyProperty(outputModel,instance.Name,instance.Type,upstreamConcept.Properties);
                    
                    CopyPropertyAsattribute(outputModel,instance.Name,instance.Type,instance.Properties);

                    CopyOperation(outputModel,instance.Name,instance.Type,upstreamConcept.Operations);

                    CopyOperation(outputModel,instance.Name,instance.Type,instance.Operations);
                }
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }
    private void CopyOperation(DomainModel model, string ConceptName, string ConceptType, IEnumerable<Operation> operations)
    {
        foreach(var operation in  operations){
            model.AddOperation(ConceptName,ConceptType,operation);
        }
    }

    private void CopyProperty(DomainModel model,string ConceptName,string ConceptType, IEnumerable<Property> properties)
    {
        foreach(var prop in properties)
        {
            model.TryAddProperty(ConceptName,ConceptType,prop);
        }
    }
    private void CopyPropertyAsattribute(DomainModel model,string ConceptName,string ConceptType, IEnumerable<Property> properties)
    {
        foreach(var prop in properties)
        {
            if(!prop.Value.IsNull && prop.Value.Value!=null)
                model.TrySetAttribute(ConceptName,ConceptType,prop.Name.Value,prop.Value.Value);
        }
    }

}
