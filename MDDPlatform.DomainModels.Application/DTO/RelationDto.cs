using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Application.DTO;
public class RelationDto
{
    public string Name { get; set;}
    public string Target { get; set;}
    public string Multiplicity { get; set;}

    public RelationDto(string name, string target, string multiplicity)
    {
        Name = name;
        Target = target;
        Multiplicity = multiplicity;
    }
    public static RelationDto CreateFrom(Relation rel)
    {
        return new(rel.Name, rel.Target, rel.Multiplicity.Value);
    }

    public Relation ToRelation()
    {
        return Relation.Create(Name, Target, Multiplicity);
    }
}