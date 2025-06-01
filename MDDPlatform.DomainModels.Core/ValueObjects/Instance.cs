using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class Instance : ValueObject
{
    public Guid Id { get; }
    public ConceptName Name { get; }
    public ConceptType Type { get; }
    public ConceptFullName FullName => ConceptFullName.Create(Name, Type);

    private Instance(Guid id, ConceptName name, ConceptType type)
    {
        Id = id;
        Name = name;
        Type = type;
    }
    public static Instance Create(Guid id, string name, string type)
    {
        var conceptName = new ConceptName(name);
        var conceptType = new ConceptType(type);
        return new Instance(id, conceptName, conceptType);
    }
    public static Instance CreateFrom(DomainObject domainObject)
    {
        return Create(domainObject.Id, domainObject.Name, domainObject.Type);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FullName.Value.Trim().ToLower();
    }
}