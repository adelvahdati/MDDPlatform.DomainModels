using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models
{
    public class RelationDocument
    {
        public string Name { get; set;}
        public string Target { get; set;}
        public string Multiplicity { get;set; }

        private RelationDocument(string name, string target, string multiplicity)
        {
            Name = name;
            Target = target;
            Multiplicity = multiplicity;
        }
        internal Relation ToRelation(){
            var relation =  Relation.Create(Name,Target,Multiplicity);

            return relation;
        }

        internal static RelationDocument CreateFrom(Relation rel)
        {
            return new(rel.Name,rel.Target,rel.Multiplicity.Value);
        }
    }
}