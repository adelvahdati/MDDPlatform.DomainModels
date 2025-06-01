using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class DomainObjectRelationalDimension : ValueObject{
    public string Name {get; set;}
    public string Target {get;set;}

    public DomainObjectRelationalDimension(string name, string target)
    {
        Name = name;
        Target = target;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Target;
    }
}