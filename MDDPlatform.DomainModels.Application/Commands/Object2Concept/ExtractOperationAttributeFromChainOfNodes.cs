namespace MDDPlatform.DomainModels.Application.Commands;
public class ExtractOperationAttributeFromChainOfNodes : ModelOperationPattern
{
    public Guid InputModel { get;set;}
    public string FirstNode {get;set;}  // BaseClass
    public string MiddleNode {get;set;} // Operation
    public string LastNode {get;set;}   // OperationBody
    public string FirstToMiddleNodeRelation {get;set;}  // hasOperation
    public string MiddleToLastNodeRelation {get;set;}   // hasBody
    public Guid OutputModel {get;set;}
    public string ConceptNameExpression {get;set;}  // FirstNode.Name
    public string ConceptTypeExpression {get;set;}  // FirstNode._Type
    public string OperationNameExpression{get;set;} // MiddleNode.Name
    public string AttributeNameExpression {get;set;}    // body
    public string AttributeValueExpression {get;set;}   // LastNode.Text

    public ExtractOperationAttributeFromChainOfNodes(Guid inputModel, string firstNode, string middleNode, string lastNode, string firstToMiddleNodeRelation, string middleToLastNodeRelation, Guid outputModel, string conceptNameExpression, string conceptTypeExpression, string operationNameExpression, string attributeNameExpression, string attributeValueExpression)
    {
        InputModel = inputModel;
        FirstNode = firstNode;
        MiddleNode = middleNode;
        LastNode = lastNode;
        FirstToMiddleNodeRelation = firstToMiddleNodeRelation;
        MiddleToLastNodeRelation = middleToLastNodeRelation;
        OutputModel = outputModel;
        ConceptNameExpression = conceptNameExpression;
        ConceptTypeExpression = conceptTypeExpression;
        OperationNameExpression = operationNameExpression;
        AttributeNameExpression = attributeNameExpression;
        AttributeValueExpression = attributeValueExpression;
    }
}