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
public class MergeUpstreamNodePropertyWithDownStreamNodeRelationHandler : ICommandHandler<MergeUpstreamNodePropertyWithDownStreamNodeRelation>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public MergeUpstreamNodePropertyWithDownStreamNodeRelationHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(MergeUpstreamNodePropertyWithDownStreamNodeRelation command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(MergeUpstreamNodePropertyWithDownStreamNodeRelation command)
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

    private async Task HandleModelOperationAsync(MergeUpstreamNodePropertyWithDownStreamNodeRelation command)
    {
        DomainModel downStreamModel = await _domainModelRepository.GetDomainModelAsync(command.DownStreamModel);
        var sourceNodeInstances = downStreamModel.GetDomainObjects(command.SourceNode);

        DomainModel upStreamModel = await _domainModelRepository.GetDomainModelAsync(command.UpStreamModel);

        DomainModel outputModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);

        foreach(var instance in sourceNodeInstances)
        {
            outputModel.TryCreateDomainConcept(instance.Name,instance.Type);

            var upStreamTargetInstances = instance.GetRelationalTargetInstances(command.SourceToDestinationRelationalDimension);
            foreach(var upstreamTargetInstance in upStreamTargetInstances)
            {
                var upstreamConcept = upStreamModel.GetDomainConcept(ConceptFullName.Create(upstreamTargetInstance));
                if(!Equals(upstreamConcept,null))
                {
                    var properties = upstreamConcept.Properties;
                    foreach(var property in properties)
                    {
                        outputModel.TryAddProperty(instance.Name,instance.Type,property);
                    }

                    var relations = upstreamConcept.Relations;
                    foreach(var relation in relations)
                    {
                        var multiplicity = relation.Multiplicity;
                        bool isCollection;
                        if(multiplicity.Value == RelationMultiplicity.ExactlyOne().Value || multiplicity.Value == RelationMultiplicity.AtMostOne().Value)
                            isCollection = false;
                        else
                            isCollection = true;
                        
                        var property = Property.Create(relation.Name.Value,relation.Target.Value,isCollection);
                        var propType = property.Type.IsCollection ? string.Format("{0}[]",property.Type.Value) : property.Type.Value;
                        outputModel.TryAddProperty(instance.Name,instance.Type,property);                        
                    }
                }
            }
            var instanceRelations = instance.Relations.Where(r=>r.Name.Value.ToLower() == command.SourceNodeRelationName.ToLower());
            foreach(var relation in instanceRelations){
                var targetInstances = relation.GetTargetInstances();
                foreach(var targetInstance in targetInstances)
                {
                    var prop = Property.Create(targetInstance.Name,targetInstance.Type);
                    outputModel.TryAddProperty(instance.Name,instance.Type,prop);
                }
            }
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputModel);
    }
}