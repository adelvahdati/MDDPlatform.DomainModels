using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
// Input => PSM BaseCalss -> Package
// Output => Set packgaes that are used as a "using" attribute
public class ReplaceRelationWithConceptAttribute : ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string SourceNode {get;set;}
    public string DestinationNode {get;set;}
    public string SourceToDestinationRelation {get;set;}
    public string AttributeNameExpression {get;set;}
    public string AttributeValueExpression {get;set;}
    public Guid OutputModel {get;set;}

    public ReplaceRelationWithConceptAttribute(Guid inputModel, string sourceNode, string destinationNode, string sourceToDestinationRelation, string attributeNameExpression, string attributeValueExpression, Guid outputModel)
    {
        InputModel = inputModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelation = sourceToDestinationRelation;
        AttributeNameExpression = attributeNameExpression;
        AttributeValueExpression = attributeValueExpression;
        OutputModel = outputModel;
    }
}