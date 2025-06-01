using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class ReplaceRelationWithChainOfNodes : ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string SourceNode {get;set;}    
    public string DestinationNode {get;set;}
    public string SourceToDestinationRelation {get;set;}
    public Guid OutputModel {get;set;}
    public string FirstNode {get;set;}
    public string MiddleNode {get;set;}
    public string LastNode {get;set;}
    
    public string FirstToMiddleNodeRelation {get;set;}
    public string MiddleToLastNodeRelation {get;set;}
    
    public string FirstNodeInstanceExpression { get; set; }
    public string MiddleNodeInstanceExpression {get;set;}
    public string LastNodeInstanceExpression {get;set;}

    public ReplaceRelationWithChainOfNodes(Guid inputModel, string sourceNode, string destinationNode, string sourceToDestinationRelation, Guid outputModel, string firstNode, string middleNode, string lastNode, string firstToMiddleNodeRelation, string middleToLastNodeRelation, string firstNodeInstanceExpression, string middleNodeInstanceExpression, string lastNodeInstanceExpression)
    {
        InputModel = inputModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelation = sourceToDestinationRelation;
        OutputModel = outputModel;
        FirstNode = firstNode;
        MiddleNode = middleNode;
        LastNode = lastNode;
        FirstToMiddleNodeRelation = firstToMiddleNodeRelation;
        MiddleToLastNodeRelation = middleToLastNodeRelation;
        FirstNodeInstanceExpression = firstNodeInstanceExpression;
        MiddleNodeInstanceExpression = middleNodeInstanceExpression;
        LastNodeInstanceExpression = lastNodeInstanceExpression;
    }
}