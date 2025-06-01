using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class MapRelatedObjects : ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string InputSource {get;set;}
    public string InputDestination {get;set;}
    public string InputSourceToDestinationRelation {get;set;}
    public Guid OutputModel {get;set;}
    public string OutputSource {get;set;}
    public string OutputDestination {get;set;}
    public string OutputSourceToDestinationRelation {get;set;}
    public string OutputSourceInstanceExpression {get;set;}
    public string OutputDestinationInstanceExpression {get;set;}

    public MapRelatedObjects(Guid inputModel, string inputSource, string inputDestination, string inputSourceToDestinationRelation, Guid outputModel, string outputSource, string outputDestination, string outputSourceToDestinationRelation, string outputSourceInstanceExpression, string outputDestinationInstanceExpression)
    {
        InputModel = inputModel;
        InputSource = inputSource;
        InputDestination = inputDestination;
        InputSourceToDestinationRelation = inputSourceToDestinationRelation;
        OutputModel = outputModel;
        OutputSource = outputSource;
        OutputDestination = outputDestination;
        OutputSourceToDestinationRelation = outputSourceToDestinationRelation;
        OutputSourceInstanceExpression = outputSourceInstanceExpression;
        OutputDestinationInstanceExpression = outputDestinationInstanceExpression;
    }
}