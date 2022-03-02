namespace MDDPlatform.DomainModels.Application.DTO{
    public class NewConceptDto{
        public string Name { get; set;}
        public string Type { get; set;}

        public NewConceptDto(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}