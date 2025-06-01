using MDDPlatform.BaseConcepts.ValueObjects;
namespace MDDPlatform.DomainModels.Application.DTO;
public class DomainObjectRelationDto
{
    public string RelationName { get; set;}
    public string RelationTarget { get; set;}
    public string Multiplicity { get; set;}
    public List<string> TargetInstances { get; set;}

    public DomainObjectRelationDto(string relationName, string relationTarget, string multiplicity, List<string> targetInstances)
    {
        RelationName = relationName;
        RelationTarget = relationTarget;
        Multiplicity = multiplicity;
        TargetInstances = targetInstances;
    }
    public static DomainObjectRelationDto CreateFrom(Relation relation)
    {
        var relationName = relation.Name.Value;
        var relationTarget = relation.Target.Value;
        var multiplicity = relation.Multiplicity.Value;
        List<string> targetInstances = relation.GetTargetInstances()
                                        .Select(targetInstance => targetInstance.FullName.Value)
                                        .ToList();

        if (targetInstances == null)
            targetInstances = new List<string>();

        return new(relationName, relationTarget, multiplicity, targetInstances);
    }
    public Relation ToRelation()
    {
        Relation relation = Relation.Load(RelationName, RelationTarget, Multiplicity,TargetInstances);
        return relation;
    }
}