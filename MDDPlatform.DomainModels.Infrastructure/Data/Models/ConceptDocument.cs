using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models
{
    public class ConceptDocument
    {
        public Guid Id {get;set;}
        public string Name { get; set;}
        public string Type { get; set;}

        public ConceptDocument(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        internal static ConceptDocument CreateFrom(Concept c)
        {
            return new ConceptDocument(c.Id,c.Name.Value,c.Type.Value);
        }
        public Concept ToConcept(){
            var concept  = Concept.Create(Id,Name,Type);
            return concept;
        }
    }
}