using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class DomainObjectProperty : ValueObject
{
    public string Name { get; set; }
    public string Value {get;set;}

    public DomainObjectProperty(string name, string value)
    {
        Name = name;
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}