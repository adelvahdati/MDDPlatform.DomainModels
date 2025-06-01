using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models;
public class DomainConceptDocument : BaseEntity<Guid>
{
    public Guid DomainId {get; set;}
    public Guid ConceptId {get; set;}
    public string Name {get; set;}
    public string Type {get; set;}
    public List<DomainObjectDocument> Instances {get; set;}
    public List<PropertyDocument> Properties {get; set;}
    public List<RelationDocument> Relations {get; set;}
    public  List<OperationDocument> Operations {get; set;}
    public List<AttributeDocument> Attributes { get; set; }

    private DomainConceptDocument(Guid domainConceptId, Guid domainId, Guid conceptId, string name, string type, List<DomainObjectDocument> instances, List<PropertyDocument> properties, List<RelationDocument> relations, List<OperationDocument> operations,List<AttributeDocument>? attributes = null)
    {
        Id = domainConceptId;
        DomainId = domainId;
        ConceptId = conceptId;
        Name = name;
        Type = type;
        Instances = instances;
        Properties = properties;
        Relations = relations;
        Operations = operations;
        Attributes = attributes == null ? new() : attributes;
    }
    public static DomainConceptDocument CreateFrom(DomainConcept domainConcept ){
        var domainConceptId = domainConcept.Id;
        var domainId = domainConcept.DomainId;
        var conceptId = domainConcept.ConceptId;
        var instances = domainConcept.Instances;
        var properties = domainConcept.Properties;
        var relations = domainConcept.Relations;
        var operations = domainConcept.Operations;
        var attributes = domainConcept.Attributes;

        var instanceDocuments = instances.Select(instance=> DomainObjectDocument.CreateFrom(instance)).ToList();
        var propertyDocuments = properties.Select(prop=>PropertyDocument.CreateFrom(prop)).ToList();
        var relationDocuments = relations.Select(rel=>RelationDocument.CreateFrom(rel)).ToList();
        var operationDocuments = operations.Select(operation=> OperationDocument.CreateFrom(operation)).ToList();
        var attributeDocuments = attributes.Select(attr=>AttributeDocument.CreateFrom(attr)).ToList();

        return new(domainConceptId,domainId,conceptId,domainConcept.Name,domainConcept.Type,instanceDocuments,propertyDocuments,relationDocuments,operationDocuments,attributeDocuments);
    }
    public DomainConcept ToDomainConcept()
    {
        List<Property>? properties = Properties.Count > 0 ?
                                            Properties.Select(propDoc=>propDoc.ToProperty()).ToList() :
                                            null;
        List<Relation>? relations = Relations.Count > 0 ?
                                            Relations.Select(relDoc=>relDoc.ToRelation()).ToList() :
                                            null;

        List<Operation>? operations = Operations.Count > 0 ?
                                            Operations.Select(oprDoc=>oprDoc.ToOperation()).ToList() :
                                            null;
        List<DomainObject> instances = Instances.Count>0 ?
                                            Instances.Select(instance=>instance.ToDomainObejct()).ToList() :
                                            new List<DomainObject>();

        BaseConcept concept = BaseConcept.Load(ConceptId,Name,Type,properties,relations,operations);
        DomainConcept domainConcept = DomainConcept.Load(Id,DomainId,concept,instances);
        if(Attributes == null)
            return domainConcept;
                        
        foreach(var attribute in Attributes){
            domainConcept.SetAttribute(attribute.Name,attribute.Value);
        }

        return domainConcept;
    }
}