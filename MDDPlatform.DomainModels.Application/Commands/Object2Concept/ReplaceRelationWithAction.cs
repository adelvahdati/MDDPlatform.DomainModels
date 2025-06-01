using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
/*
    Used for :
        1- Creating constructor
        2 - CommandHandler Handle Method
        3 - EventHandler Handle Method
*/
public class ReplaceRelationWithAction : ModelOperationPattern
{
    public Guid InputModel { get; set;}
    public string ConceptNode { get; set;}
    public string OperationNameProperty { get; set;}
    public string OperationOutputProperty { get; set;} // Task, Void, ""
    public string OperationInputsRelation {get;set;}
    public Guid OutputModel { get; set;}

    public ReplaceRelationWithAction(Guid inputModel, string conceptNode, string operationNameProperty, string operationOutputProperty, string operationInputsRelation, Guid outputModel)
    {
        InputModel = inputModel;
        ConceptNode = conceptNode;
        OperationNameProperty = operationNameProperty;
        OperationOutputProperty = operationOutputProperty;
        OperationInputsRelation = operationInputsRelation;
        OutputModel = outputModel;
    }
}