namespace MDDPlatform.DomainModels.Application.Commands;
public class ExtractOperationFromOneToOneToManyRelation : ModelOperationPattern
{
    public Guid InputModel { get;set;}
    public string FirstNode {get;set;}  // BaseClass.Concept
    public string MiddleNode {get;set;} // Operation.Concept
    public string LastNode {get;set;}   // Parameter.Concept
    public string FirstToMiddleNodeRelation {get;set;}  // hasOperation
    public string MiddleToLastNodeRelation {get;set;}   // hasInput
    public string OperationNameExpression {get;set;}    // MiddleNode.OperationName
    public string OperationInputNameExpression {get;set;}  // LastNode.ParameterName
    public string OperationInputTypeExpression {get;set;}  // LastNode.ParameterType
    public string OperationOutputTypeExpression {get;set;}  // MiddleNode.OperationOutput
    public Guid OutputModel {get;set;}

    public ExtractOperationFromOneToOneToManyRelation(Guid inputModel, string firstNode, string middleNode, string lastNode, string firstToMiddleNodeRelation, string middleToLastNodeRelation, string operationNameExpression, string operationInputNameExpression, string operationInputTypeExpression, string operationOutputTypeExpression, Guid outputModel)
    {
        InputModel = inputModel;
        FirstNode = firstNode;
        MiddleNode = middleNode;
        LastNode = lastNode;
        FirstToMiddleNodeRelation = firstToMiddleNodeRelation;
        MiddleToLastNodeRelation = middleToLastNodeRelation;
        OperationNameExpression = operationNameExpression;
        OperationInputNameExpression = operationInputNameExpression;
        OperationInputTypeExpression = operationInputTypeExpression;
        OperationOutputTypeExpression = operationOutputTypeExpression;
        OutputModel = outputModel;
    }
}