using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application.DTO;
public class ElementDto
{
    public Guid Id {get;set;}
    public string Name {get;set;}
    public string Type {get;set;}
    public List<PropertyDto> Properties {get;set;}
    public List<RelationDto> Relations {get;set;}
    public  List<OperationDto> Operations {get;set;}
    public List<AttributeDto> Attributes { get; set; }

    public ElementDto(Guid id, string name, string type, List<PropertyDto> properties, List<RelationDto> relations, List<OperationDto> operations, List<AttributeDto>? attributes = null)
    {
        Id = id;
        Name = name;
        Type = type;
        Properties = properties;
        Relations = relations;
        Operations = operations;
        Attributes = attributes == null ? new() : attributes;
    }
    internal static ElementDto CreateFrom(DomainConcept domainConcept)
    {
        var domainConceptId = domainConcept.Id;
        var properties = domainConcept.Properties;
        var relations = domainConcept.Relations;
        var operations = domainConcept.Operations;
        var attributes = domainConcept.Attributes;

        var propertyDtos = properties.Select(prop=>PropertyDto.CreateFrom(prop)).ToList();
        var relationDtos = relations.Select(rel=>RelationDto.CreateFrom(rel)).ToList();
        var operationDtos = operations.Select(operation=> OperationDto.CreateFrom(operation)).ToList();
        var attributeDtos = attributes.Select(attr=>AttributeDto.CreateFrom(attr)).ToList();

        return new(domainConceptId,domainConcept.Name,domainConcept.Type,propertyDtos,relationDtos,operationDtos,attributeDtos);
    }

}