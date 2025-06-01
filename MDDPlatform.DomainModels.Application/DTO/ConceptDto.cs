using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Application.DTO;
public class ConceptDto{
    public Guid Id {get;set;}
    public string Name { get; set;}
    public string Type { get; set;}

    public ConceptDto(Guid id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }

    internal static ConceptDto CreateFrom(Concept concept)
    {
        return new ConceptDto(concept.Id,concept.Name,concept.Type);
    }
    internal static ConceptDto CreateFrom(DomainConcept domainConcept)
    {
        return new ConceptDto(domainConcept.Id,domainConcept.Name,domainConcept.Type);
    }
    public Concept ToConcept(){
        return Concept.Create(Id,Name,Type);
    }
}