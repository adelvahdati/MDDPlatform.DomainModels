using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects
{

    public class Concept : ValueObject
    {
        public Guid Id {get;}
        public ConceptName Name { get; }
        public ConceptType Type { get; }
        public ConceptFullName FullName => ConceptFullName.Create(Name,Type);
        private Concept(Guid id,ConceptName name, ConceptType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
        public static Concept Create(Guid id,string name, string type)
        {            
            var conceptName = new ConceptName(name);
            var conceptType = new ConceptType(type);
            return new Concept(id,conceptName,conceptType);
        }
        public static Concept CreateFrom(DomainConcept domainConcept)
        {
            return Create(domainConcept.Id,domainConcept.Name,domainConcept.Type);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FullName.Value.Trim().ToLower();
        }
    }
}