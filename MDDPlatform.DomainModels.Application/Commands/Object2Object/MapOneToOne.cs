namespace MDDPlatform.DomainModels.Application.Commands;
public class MapOneToOne : ModelOperationPattern
{
        public Guid InputModel { get; set;}
        public string Source {get;set;}
        public Guid OutputModel { get; set;}
        public string Destination {get;set;}

    public MapOneToOne(Guid inputModel, string source, Guid outputModel, string destination)
    {
        InputModel = inputModel;
        Source = source;
        OutputModel = outputModel;
        Destination = destination;
    }
}
