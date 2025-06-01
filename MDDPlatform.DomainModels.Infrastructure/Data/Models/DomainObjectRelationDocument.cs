using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models;
public class DomainObjectRelationDocument
{
    public string RelationName { get; set;}
    public string RelationTarget { get; set;}
    public string Multiplicity { get; set;}
    public List<string> TargetInstances { get; set;}

    private DomainObjectRelationDocument(string relationName, string relationTarget, string multiplicity, List<string> targetInstances)
    {
        RelationName = relationName;
        RelationTarget = relationTarget;
        Multiplicity = multiplicity;
        TargetInstances = targetInstances;
    }
    public static DomainObjectRelationDocument CreateFrom(Relation relation)
    {
        var relationName = relation.Name.Value;
        var relationTarget = relation.Target.Value;
        var multiplicity = relation.Multiplicity.Value;
        List<string> targetInstances = relation.GetTargetInstances()
                                        .Select(targetInstance => targetInstance.Name)
                                        .ToList();

        if (targetInstances == null)
            targetInstances = new List<string>();

        return new(relationName, relationTarget, multiplicity, targetInstances);
    }
    public Relation ToRelation()
    {
        Relation relation = Relation.Create(RelationName, RelationTarget, Multiplicity);
        foreach (var targetInstance in TargetInstances)
        {
            relation.SetTargetInstance(targetInstance);
        }
        return relation;
    }
}