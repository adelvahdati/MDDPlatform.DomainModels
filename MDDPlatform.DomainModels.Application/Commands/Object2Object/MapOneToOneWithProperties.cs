namespace MDDPlatform.DomainModels.Application.Commands;

public class MapOneToOneWithProperties : ModelOperationPattern
{
    public Guid InputModel { get; set; }
    public string Source { get; set; }
    public Guid OutputModel { get; set; }
    public string Destination { get; set; }
    public List<Member> DestinationMembers { get; set; }

    public MapOneToOneWithProperties(Guid inputModel, string source, Guid outputModel, string destination, List<Member> destinationMembers)
    {
        InputModel = inputModel;
        Source = source;
        OutputModel = outputModel;
        Destination = destination;
        DestinationMembers = destinationMembers;
    }
}
public class Member
{
    public string Name { get; set; }
    public string ValueExpression { get; set; }

    public Member(string name, string valueExpression)
    {
        Name = name;
        ValueExpression = valueExpression;
    }
}