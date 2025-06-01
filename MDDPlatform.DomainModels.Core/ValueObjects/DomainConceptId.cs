using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class DomainConceptId : ValueObject
{
    public Guid Value {get;}

    public DomainConceptId(Guid domainConceptId)
    {
        Value = domainConceptId;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}