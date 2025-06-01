using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class CreateDomainObject : ICommand
{
    public Guid DomainModelId {get;set;}
    public Guid DomainConceptId {get;set;}
    public string InstanceName {get;set;}
    public List<DomainObjectProperty> DomainObjectProperties { get; set;}
    public List<DomainObjectRelation> DomainObjectRelations { get; set;}
    public List<DomainObjectRelationalDimension> RelationalDimensions {get;set;}

    public CreateDomainObject(Guid domainModelId,Guid domainConceptId, string instanceName, List<DomainObjectProperty> domainObjectProperties, List<DomainObjectRelation> domainObjectRelations,List<DomainObjectRelationalDimension> relationalDimensions)
    {
        DomainConceptId = domainConceptId;
        InstanceName = instanceName;
        DomainObjectProperties = domainObjectProperties;
        DomainObjectRelations = domainObjectRelations;
        DomainModelId = domainModelId;
        RelationalDimensions = relationalDimensions;
    }
}