using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class DomainModelId : ValueObject
{
    public Guid Value {get;}

    public DomainModelId(Guid modelId)
    {
        Value = modelId;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}