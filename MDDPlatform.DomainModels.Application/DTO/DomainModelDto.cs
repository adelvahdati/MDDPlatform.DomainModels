namespace MDDPlatform.DomainModels.Application.DTO
{
    public class DomainModelDto
    {
        public Guid Id {get;set;}
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Type {get;set;}
        public int Level {get;set;}
        public Guid DomainId {get;set;}
        public List<ConceptDto> Concepts {get;set;}

        public DomainModelDto(Guid id, string name, string tag, string type, int level, Guid domainId, List<ConceptDto> concepts)
        {
            Id = id;
            Name = name;
            Tag = tag;
            Type = type;
            Level = level;
            DomainId = domainId;
            Concepts = concepts;
        }
    }
}