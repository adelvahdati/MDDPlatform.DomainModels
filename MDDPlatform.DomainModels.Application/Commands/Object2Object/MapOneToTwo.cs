namespace MDDPlatform.DomainModels.Application.Commands;
public class MapOneToTwo : ModelOperationPattern
{
    public Guid InputModel { get; set;}
    public string Source {get;set;}
    public string FirstDestination {get;set;}
    public string FirstDestinationInstanceNameExpression {get;set;}
    public string SecondDestination {get;set;}
    public string SecondDestinationInstanceNameExpression {get;set;}
    public string FirstToSecondRelationName {get;set;}

    public List<Member> FirstDestinationMembers {get;set;}
    public List<Member> SecondDestinationMembers {get;set;}

    public Guid OutputModel { get; set;}

    public MapOneToTwo(Guid inputModel, string source, string firstDestination, string firstDestinationInstanceNameExpression, string secondDestination, string secondDestinationInstanceNameExpression, string firstToSecondRelationName, List<Member> firstDestinationMembers, List<Member> secondDestinationMembers, Guid outputModel)
    {
        InputModel = inputModel;
        Source = source;
        FirstDestination = firstDestination;
        FirstDestinationInstanceNameExpression = firstDestinationInstanceNameExpression;
        SecondDestination = secondDestination;
        SecondDestinationInstanceNameExpression = secondDestinationInstanceNameExpression;
        FirstToSecondRelationName = firstToSecondRelationName;
        FirstDestinationMembers = firstDestinationMembers;
        SecondDestinationMembers = secondDestinationMembers;
        OutputModel = outputModel;
    }
}
