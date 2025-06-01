namespace MDDPlatform.DomainModels.Application.Commands;
// Exctract Operation From One to One to One Relation
public class ExtractOperationFromChainOfNodes : ModelOperationPattern   
{
    public Guid InputModel { get;set;}
    public string FirstNode {get;set;}  // QueryHandler
    public string MiddleNode {get;set;} // Query
    public string LastNode {get;set;}   // QueryResult
    public string FirstToMiddleNodeRelation {get;set;}  // canHandle
    public string MiddleToLastNodeRelation {get;set;}   // return
    public string OperationNameExpression {get;set;}    // Handle
    public string OperationInputsExpression {get;set;}  // FirstNode.canHandle(Query.Concept)
    public string OperationOutputTypeExpression {get;set;}  // LastNode.ResultType
    public string OperationOutputMultiplicityExpression {get;set;}  // LastNode.Multiplicity
    public Guid OutputModel {get;set;}

    public ExtractOperationFromChainOfNodes(string firstNode, string middleNode, string lastNode, string firstToMiddleNodeRelation, string middleToLastNodeRelation, string operationNameExpression, string operationInputsExpression, string operationOutputTypeExpression, string operationOutputMultiplicityExpression)
    {
        FirstNode = firstNode;
        MiddleNode = middleNode;
        LastNode = lastNode;
        FirstToMiddleNodeRelation = firstToMiddleNodeRelation;
        MiddleToLastNodeRelation = middleToLastNodeRelation;
        OperationNameExpression = operationNameExpression;
        OperationInputsExpression = operationInputsExpression;
        OperationOutputTypeExpression = operationOutputTypeExpression;
        OperationOutputMultiplicityExpression = operationOutputMultiplicityExpression;
    }
}