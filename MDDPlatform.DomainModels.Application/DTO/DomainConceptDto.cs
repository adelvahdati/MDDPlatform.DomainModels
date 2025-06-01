using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application.DTO;
public class DomainConceptDto 
{
    public Guid Id {get;set;}
    public Guid DomainId {get;set;}
    public Guid ConceptId {get;set;}
    public string Name {get;set;}
    public string Type {get;set;}
    public List<InstanceDto> Instances {get;set;}
    public List<PropertyDto> Properties {get;set;}
    public List<RelationDto> Relations {get;set;}
    public  List<OperationDto> Operations {get;set;}
    public List<AttributeDto> Attributes { get; set; }


    public DomainConceptDto(Guid domainConceptId, Guid domainId, Guid conceptId, string name, string type, List<InstanceDto> instances, List<PropertyDto> properties, List<RelationDto> relations, List<OperationDto> operations,List<AttributeDto>? attributes = null)
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

    internal static DomainConceptDto CreateFrom(DomainConcept domainConcept)
    {
        var domainConceptId = domainConcept.Id;
        var domainId = domainConcept.DomainId;
        var conceptId = domainConcept.ConceptId;
        var instances = domainConcept.Instances;
        var properties = domainConcept.Properties;
        var relations = domainConcept.Relations;
        var operations = domainConcept.Operations;
        var attributes = domainConcept.Attributes;

        var instanceDtos = instances.Select(instance=> InstanceDto.CreateFrom(instance)).ToList();
        var propertyDtos = properties.Select(prop=>PropertyDto.CreateFrom(prop)).ToList();
        var relationDtos = relations.Select(rel=>RelationDto.CreateFrom(rel)).ToList();
        var operationDtos = operations.Select(operation=> OperationDto.CreateFrom(operation)).ToList();
        var attributeDtos = attributes.Select(attr=>AttributeDto.CreateFrom(attr)).ToList();

        return new(domainConceptId,domainId,conceptId,domainConcept.Name,domainConcept.Type,instanceDtos,propertyDtos,relationDtos,operationDtos,attributeDtos);
    }
}