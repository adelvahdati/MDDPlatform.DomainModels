using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models;
public class DomainObjectDocument : BaseEntity<Guid>
{
    public Guid DomainConceptId {get; set;}
    public string InstanceName {get; set;}
    public string InstanceType {get; set;}
    public List<PropertyDocument> Properties {get; set;}
    public List<DomainObjectRelationDocument> Relations {get; set;}
    public  List<OperationDocument> Operations {get; set;}
    public List<RelationalDimensionDocument> RelationalDimensions {get; set;}

    public string FullName()
    {
        return ConceptFullName.Create(InstanceName,InstanceType).Value;
    }
    private DomainObjectDocument(Guid domainObjectId ,Guid domainConceptId, string instanceName, string instanceType, List<PropertyDocument> properties, List<DomainObjectRelationDocument> relations, List<OperationDocument> operations, List<RelationalDimensionDocument>? relationalDimensions=null)
    {
        Id = domainObjectId;
        DomainConceptId = domainConceptId;
        InstanceName = instanceName;
        InstanceType = instanceType;
        Properties = properties;
        Relations = relations;
        Operations = operations;
        RelationalDimensions = relationalDimensions == null? new() : relationalDimensions;
    }
    public static DomainObjectDocument CreateFrom(DomainObject domainObject ){
        
        var domainConceptId = domainObject.DomainConceptId;
        var instanceName = domainObject.Name;
        var instanceType = domainObject.Type;
        
        var properties = domainObject.Properties
                                        .Select(property=>PropertyDocument.CreateFrom(property))
                                        .ToList();
        var relations = domainObject.Relations
                                        .Select(relation=> DomainObjectRelationDocument.CreateFrom(relation))
                                        .ToList();
        var operations = domainObject.Operations
                                        .Select(operation=>OperationDocument.CreateFrom(operation))
                                        .ToList();

        var relationalDimensions = domainObject.RelationalDimensions
                                                    .Select(relationalDimension=> RelationalDimensionDocument.CreateFrom(relationalDimension))
                                                    .ToList();

        return new DomainObjectDocument(domainObject.Id,domainConceptId.Value,instanceName,instanceType,properties,relations,operations,relationalDimensions);

    }
    public DomainObject ToDomainObejct()
    {
        var properties = Properties
                            .Select(propertyDocument=>propertyDocument.ToProperty())
                            .ToList();                            
        var relations = Relations
                            .Select(relationDocument=>relationDocument.ToRelation())
                            .ToList();
        var operations = Operations
                            .Select(operationDocument=> operationDocument.ToOperation())
                            .ToList();
        var relationalDimensions = RelationalDimensions
                                        .Select(relationalDimension=> relationalDimension.ToRelationalDimension())
                                        .ToList();
        
        BaseConcept instance = BaseConcept.Load(Id,InstanceName,InstanceType,properties,relations,operations);
        DomainObject domainObject = DomainObject.Load(instance,new DomainConceptId(this.DomainConceptId),relationalDimensions);
        return domainObject;
    }
    public bool HasRelation(string relatinName, string relationTarget)
    {
        return Relations.Exists(relation=>relation.RelationName == relatinName &&
                                            relation.RelationTarget == relationTarget &&
                                            relation.TargetInstances.Count>0);
    }
}