using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application.DTO;
public class DomainObjectDto
{
    public Guid Id {get; set;}
    public Guid DomainConceptId {get;set;}
    public string InstanceName {get;set;}
    public string InstanceType {get;set;}
    public List<PropertyDto> Properties {get;set;}
    public List<DomainObjectRelationDto> Relations {get;set;}
    public  List<OperationDto> Operations {get;set;}
    public List<RelationalDimensionDto> RelationalDimensions {get;set;}
    public DomainObjectDto(
        Guid domainObjectId ,Guid domainConceptId, 
        string instanceName, string instanceType, 
        List<PropertyDto> properties, List<DomainObjectRelationDto> relations, 
        List<OperationDto> operations, List<RelationalDimensionDto>? relationalDimensions =null)
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

    internal static DomainObjectDto CreateFrom(DomainObject domainObject)
    {
        var domainConceptId = domainObject.DomainConceptId.Value;
        var instanceName = domainObject.Name;
        var instanceType = domainObject.Type;
        
        var properties = domainObject.Properties
                                        .Select(property=>PropertyDto.CreateFrom(property))
                                        .ToList();
        var relations = domainObject.Relations
                                        .Select(relation=> DomainObjectRelationDto.CreateFrom(relation))
                                        .ToList();
        var operations = domainObject.Operations
                                        .Select(operation=>OperationDto.CreateFrom(operation))
                                        .ToList();

        var relationalDimensions = domainObject.RelationalDimensions
                                                    .Select(relationalDimension=> RelationalDimensionDto.CreateFrom(relationalDimension))
                                                    .ToList();

        return new DomainObjectDto(domainObject.Id,domainConceptId,instanceName,instanceType,properties,relations,operations,relationalDimensions);
    }
}