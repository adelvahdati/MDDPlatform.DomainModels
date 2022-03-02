using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.ActionResults;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Core.Entities
{
    public class DomainModel : BaseAggregate<Guid> 
    {
        private ISet<Concept> _concepts;
        public string Name { get; private set; }
        public string Tag { get; private set; }
        public string Type { get; private set;}
        public int Level {get;private set;}
        public IReadOnlyList<Concept> Concepts => _concepts.ToList();

        public Domain Domain { get; private set;}

        private DomainModel(Guid id ,string name, string tag,string type, int level,List<Concept> concepts, Domain domain)
        {
            Id = id;
            Name = name;
            Tag = tag;
            Type = type;
            Level = level;
            _concepts = new HashSet<Concept>(concepts);
            Domain = domain;
        }
        private DomainModel(Guid id, string name, string tag,string type, int level, Domain domain)
        {
            Id = id;
            Name = name;
            Tag = tag;
            Type = type;
            Level = level;
            _concepts = new HashSet<Concept>();
            Domain = domain;
        }
        public static IActionResult<DomainModel> Create(Guid id, string name, string tag, string type,int level, Domain domain){
            // To Do : Check Null and empty value;

            return TheAction.IsDone<DomainModel>(new DomainModel(id,name,tag,type,level,domain));
        }
        public static IActionResult<DomainModel> Create(Guid id, string name, string tag,string type,int level,List<Concept> concepts, Domain domain){
            // To Do : Check Null and empty value;

            return TheAction.IsDone<DomainModel>(new DomainModel(id,name,tag,type,level,concepts,domain));
        }
        public IActionResult<bool> CreateConcept(string name,string type){            
            var action = Concept.Create(name,type);
            if(action.Status == ActionStatus.Failure)
                return TheAction.Failed<bool>(action.Message);
            
            var concept=action.Result;
            if(Equals(concept,null))
                return TheAction.Failed<bool>("Unexpected Result in concept creation");           

            if(!_concepts.Add(concept))
                return TheAction.Failed<bool>("Concept with this name and type exist in this abstraction level");

            AddEvent(new ConceptCreated(this,concept));
            return TheAction.IsDone<bool>(true,"Domain concept created");
        }
        public IActionResult<bool> RemoveConcept(string name,string type){

            var concept = _concepts.AsEnumerable().Where(c=>c.Name == name && c.Type == type).FirstOrDefault();
            if(Equals(concept,null))
                return TheAction.Failed<bool>("Unexpected Result in concept creation");           

            if(!_concepts.Remove(concept))
                return TheAction.Failed<bool>("The concept can not be removed");

            AddEvent(new ConceptRemoved(this,concept));
            return TheAction.IsDone<bool>(true,"Domain concept removed");
        }
        public IActionResult<bool> CreateConcept(Concept concept){            
            if(Equals(concept,null))
                return TheAction.Failed<bool>("Unexpected Result in concept creation");           

            if(!_concepts.Add(concept))
                return TheAction.Failed<bool>("Concept with this name and type exist in this abstraction level");

            AddEvent(new ConceptCreated(this,concept));
            return TheAction.IsDone<bool>(true,"Domain concept created");
        }
        public IActionResult<bool> RemoveConcept(Concept concept){

            if(Equals(concept,null))
                return TheAction.Failed<bool>("Unexpected Result in concept creation");           

            if(!_concepts.Remove(concept))
                return TheAction.Failed<bool>("The concept can not be removed");

            AddEvent(new ConceptRemoved(this,concept));
            return TheAction.IsDone<bool>(true,"Domain concept removed");
        }
    }
}