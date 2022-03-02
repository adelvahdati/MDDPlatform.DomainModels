using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.ValueObjects;

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
            return new ConceptDocument(c.TraceId.Value,c.Name,c.Type);
        }
        public Concept ToCore(){
            var action  = Concept.Create(Name,Type);
            if(action.Status == SharedKernel.ActionResults.ActionStatus.Failure)
                throw new CoreModelCreationException();
            
            if(Equals(action.Result,null))
                throw new CoreModelCreationException();
            
            Concept concept =  action.Result;
            concept.TraceId = TraceId.Load(Id);

            return concept;
        }
        public ConceptDto ToDto()
        {
            return new ConceptDto(Id,Name,Type);
        }
    }
}