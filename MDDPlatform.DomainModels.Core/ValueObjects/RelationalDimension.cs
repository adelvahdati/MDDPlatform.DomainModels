using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class RelationalDimension : ValueObject
{
    public RelationName Name {get; set;}
    public RelationTarget Target {get;set;}

    public RelationalDimension(RelationName name, RelationTarget target)
    {
        Name = name;
        Target = target;
    }
    public static RelationalDimension Create(string relationName , string relationTarget)
    {
        RelationName name = new RelationName(relationName);
        RelationTarget target = new RelationTarget(relationTarget);
        return new(name,target);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Target;
    }
}
