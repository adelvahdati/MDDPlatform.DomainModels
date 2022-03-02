using MDDPlatform.SharedKernel.ActionResults;
using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects
{

    public class Concept : TraceableValueObject
    {
        public string Name { get; }
        public string Type { get; }

        private Concept(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public static IActionResult<Concept> Create(string name, string type)
        {
            // To Do check null and empty string

            return TheAction.IsDone<Concept>(new Concept(name, type));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Type;
        }
    }
}