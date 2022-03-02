namespace MDDPlatform.DomainModels.Application.DTO{
    public class ConceptDto{
        public Guid Id {get;set;}
        public string Name { get; set;}
        public string Type { get; set;}

        public ConceptDto(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}