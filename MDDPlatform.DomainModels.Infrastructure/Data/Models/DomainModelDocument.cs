using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.ActionResults;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models
{
    public class DomainModelDocument : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Type {get;set;}
        public int Level {get;set;}
        public List<ConceptDocument> Concepts {get;set;}

        public Guid DomainId {get;set;}

        public DomainModelDocument(Guid id,string name, string tag, string type, int level, List<ConceptDocument> concepts, Guid domainId)
        {
            Id = id;
            Name = name;
            Tag = tag;
            Type = type;
            Level = level;
            Concepts = concepts;
            DomainId = domainId;
        }

        internal static DomainModelDocument CreateFrom(DomainModel domainModel)
        {
            List<ConceptDocument> concepts;

            Guid id = domainModel.Id;
            string name = domainModel.Name;
            string tag = domainModel.Tag;
            string type = domainModel.Type;
            int level = domainModel.Level;
            Guid domainId = domainModel.Domain.Id;
            if(domainModel.Concepts.Count == 0)            
                return  new DomainModelDocument(id,name,tag,type,level,new List<ConceptDocument>(),domainId);    
            
            concepts = domainModel.Concepts.Select(c=> ConceptDocument.CreateFrom(c)).ToList();
                return  new DomainModelDocument(id,name,tag,type,level,concepts,domainId);    
        }

        internal DomainModel ToCore()
        {
            List<Concept> concepts = new List<Concept>();

            if(Concepts.Count>0)
                concepts = Concepts.Select(c=> c.ToCore()).ToList();
            
            var action = DomainModel.Create(Id,Name,Tag,Type,Level,concepts,new Domain(DomainId));
            if(action.Status == ActionStatus.Failure)
                throw new CoreModelCreationException();
            
            if(Equals(action.Result ,null))
                throw new CoreModelCreationException();
            
            return action.Result;
        }
         internal DomainModelDto ToDto()
        {
            List<ConceptDto> concepts = new List<ConceptDto>();
            if(Concepts.Count>0)
                concepts = Concepts.Select(c=>c.ToDto()).ToList();
            
            return new DomainModelDto(Id,Name,Tag,Type,Level,DomainId,concepts);
        }
        internal IList<DomainConceptDto> GetDomainConcepts(){
            if(Concepts.Count == 0)
                return new List<DomainConceptDto>();
            
            var domainConcepts =  Concepts.Select(concept => new DomainConceptDto(concept.Id,concept.Name,concept.Type,DomainId,Id)).ToList();
            return domainConcepts;            
        }
        internal IList<DomainConceptDto> GetDomainConceptsWithName(string name){
            if(Concepts.Count == 0)
                return new List<DomainConceptDto>();
            
            var concepts = Concepts.Where(c=>c.Name == name).ToList();
            if(concepts.Count == 0)
                return new List<DomainConceptDto>();

            var domainConcepts =  concepts.Select(concept => new DomainConceptDto(concept.Id,concept.Name,concept.Type,DomainId,Id)).ToList();
            return domainConcepts;            
        }
        internal IList<DomainConceptDto> GetDomainConceptsOfType(string type){
            if(Concepts.Count == 0)
                return new List<DomainConceptDto>();
            
            var concepts = Concepts.Where(c=>c.Type == type).ToList();
            if(concepts.Count == 0)
                return new List<DomainConceptDto>();

            var domainConcepts =  concepts.Select(concept => new DomainConceptDto(concept.Id,concept.Name,concept.Type,DomainId,Id)).ToList();
            return domainConcepts;            
        }
        internal DomainConceptDto GetDomainConceptById(Guid conceptId){
            if(Concepts.Count == 0)
                return null;
            
            var concept = Concepts.Find(c=>c.Id == conceptId);
            if(concept != null)
                return new DomainConceptDto(concept.Id,concept.Name,concept.Type,DomainId,Id);
            
            return null;
        }
        internal DomainConceptDto GetDomainConcept(string name,string type){
            if(Concepts.Count == 0)
                return null;
            
            var concept = Concepts.Find(c=>c.Name == name && c.Type == type);
            if(concept != null)
                return new DomainConceptDto(concept.Id,concept.Name,concept.Type,DomainId,Id);
            
            return null;
        }
   }
}