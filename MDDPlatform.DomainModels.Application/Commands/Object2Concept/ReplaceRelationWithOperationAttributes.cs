using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class ReplaceRelationWithOperationAttributes : ModelOperationPattern
{
    public Guid InputModel { get; set;}
    public string ConceptNode { get; set;}
    public string OperationNode { get; set;}
    public string OperationNameProperty { get; set;}
    public string OperationOutputProperty { get; set;}
    public string ConceptToOperationRelation { get; set;}
    public string OperationToInputParametersRelation { get; set;}
    public Guid OutputModel { get; set;}

    public ReplaceRelationWithOperationAttributes(Guid inputModel, string conceptNode, string operationNode, string operationNameProperty, string operationOutputProperty, string conceptToOperationRelation, string operationToInputParametersRelation, Guid outputModel)
    {
        InputModel = inputModel;
        ConceptNode = conceptNode;
        OperationNode = operationNode;
        OperationNameProperty = operationNameProperty;
        OperationOutputProperty = operationOutputProperty;
        ConceptToOperationRelation = conceptToOperationRelation;
        OperationToInputParametersRelation = operationToInputParametersRelation;
        OutputModel = outputModel;
    }
}