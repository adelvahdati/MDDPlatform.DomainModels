using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class ReplaceRelationWithGenericProperty : ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string SourceNode {get;set;}
    public string DestinationNode {get;set;}
    public string SourceToDestinationRelation {get;set;}

    //Output
    public Guid OutputModel {get;set;}
    public string PropertyName {get;set;}
    public string PropertyType {get;set;}

    public ReplaceRelationWithGenericProperty(Guid inputModel, string sourceNode, string destinationNode, string sourceToDestinationRelation, Guid outputModel, string propertyName, string propertyType)
    {
        InputModel = inputModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelation = sourceToDestinationRelation;
        OutputModel = outputModel;
        PropertyName = propertyName;
        PropertyType = propertyType;
    }
}