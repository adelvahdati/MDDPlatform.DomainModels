using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models
{
    public class DomainModelDocument : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Type {get;set;}
        public int Level {get;set;}
        public List<DomainConceptDocument> DomainConcepts {get;set;}

        public Guid DomainId {get;set;}

        private DomainModelDocument(Guid id,string name, string tag, string type, int level, List<DomainConceptDocument> domainConcepts, Guid domainId)
        {
            Id = id;
            Name = name;
            Tag = tag;
            Type = type;
            Level = level;
            DomainConcepts = domainConcepts;
            DomainId = domainId;
        }

        internal static DomainModelDocument CreateFrom(DomainModel domainModel)
        {
            Guid id = domainModel.Id;
            string name = domainModel.Name;
            string tag = domainModel.Tag;
            string type = domainModel.Type;
            int level = domainModel.Level;
            Guid domainId = domainModel.Domain.Id;
            var domainConcepts = domainModel.DomainConcepts;
            if(domainConcepts.Count == 0)            
                return  new DomainModelDocument(id,name,tag,type,level,new List<DomainConceptDocument>(),domainId);    
            
            var domainConceptDocuments = domainConcepts.Select(d=> DomainConceptDocument.CreateFrom(d)).ToList();
                 return  new DomainModelDocument(id,name,tag,type,level,domainConceptDocuments,domainId);    
        }

        internal DomainModel ToDomainModel()
        {
            List<DomainConcept> domainConcepts = new List<DomainConcept>();

            if(DomainConcepts.Count>0)
                 domainConcepts = DomainConcepts.Select(domainConceptDocument=> domainConceptDocument.ToDomainConcept()).ToList();
            
            var domainModel = DomainModel.Create(Id,Name,Tag,Type,Level,domainConcepts,new Domain(DomainId));
            return domainModel;
        }
   }
}