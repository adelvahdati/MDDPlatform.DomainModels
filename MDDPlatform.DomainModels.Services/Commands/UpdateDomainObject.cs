using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class UpdateDomainObject : ICommand
{
    public Guid DomainModelId {get;set;}
    public Guid DomainObjectId {get; set;}    
    public string InstanceName {get;set;}
    public List<DomainObjectProperty> Properties {get;set;}
    public List<DomainObjectRelation> Relations {get;set;}
    public List<DomainObjectRelationalDimension> RelationalDimensions {get;set;}

    public UpdateDomainObject(Guid domainModelId, Guid domainObjectId, string instanceName, List<DomainObjectProperty> properties, List<DomainObjectRelation> relations, List<DomainObjectRelationalDimension> relationalDimensions)
    {
        DomainModelId = domainModelId;
        DomainObjectId = domainObjectId;
        InstanceName = instanceName;
        Properties = properties;
        Relations = relations;
        RelationalDimensions = relationalDimensions;
    }
}