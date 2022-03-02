namespace MDDPlatform.DomainModels.Application.DTO
{
    public class DomainConceptDto
    {
        public Guid Id {get;set;}
        public string Name { get; set;}
        public string Type { get; set;}
        public Guid DomainId {get;set;}
        public Guid ModelId {get;set;}

        public DomainConceptDto(Guid id, string name, string type, Guid domainId, Guid modelId)
        {
            Id = id;
            Name = name;
            Type = type;
            DomainId = domainId;
            ModelId = modelId;
        }
    }
}