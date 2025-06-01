using MDDPlatform.BaseConcepts.Entities;

namespace MDDPlatform.DomainModels.Application.DTO;
public class LanguageElementDto{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public List<PropertyDto> Properties { get; set; }
    public List<RelationDto> Relations { get; set; }
    public LanguageElementDto(Guid id, string name, string type, List<PropertyDto> properties, List<RelationDto> relations)
    {
        Id = id;
        Name = name;
        Type = type;
        Properties = properties;
        Relations = relations;
    }
    public LanguageElementDto(){
        Id = Guid.Empty;
        Name= "";
        Type = "";
        Properties = new();
        Relations = new();
    }
    public BaseConcept ToBaseConcept(){
        var relations = Relations.Select(relationDto=> relationDto.ToRelation()).ToList();
        var properties = Properties.Select(propertyDto=> propertyDto.ToProperty()).ToList();
        return BaseConcept.Load(Id,Name,Type,properties,relations);
    }
}