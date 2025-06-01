using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class DomainObjectRelation : ValueObject
{
    public string RelationName { get; set; }
    public string RelationTarget { get; set; }
    public string TargetInstance { get; set; }

    public DomainObjectRelation(string relationName, string relationTarget, string targetInstance)
    {
        RelationName = relationName;
        RelationTarget = relationTarget;
        TargetInstance = targetInstance;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RelationName;
        yield return RelationTarget;
        yield return TargetInstance;
    }
}