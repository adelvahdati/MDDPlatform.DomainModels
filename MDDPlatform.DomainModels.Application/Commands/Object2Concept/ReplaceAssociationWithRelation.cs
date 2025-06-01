using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class ReplaceAssociationWithRelation : ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string SourceNode {get;set;} // Concept node
    public string DestinationNode {get;set;} // Association node
    public string SourceToDestinationRelation { get;set;}
    public string RelationNameProperty {get;set;}
    public string RelationTargetProperty {get;set;}
    public string MultiplicityProperty {get;set;}
    public Guid OutputModel {get;set;}

    public ReplaceAssociationWithRelation(Guid inputModel, string sourceNode, string destinationNode, string sourceToDestinationRelation, string relationNameProperty, string relationTargetProperty, string multiplicityProperty, Guid outputModel)
    {
        InputModel = inputModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelation = sourceToDestinationRelation;
        RelationNameProperty = relationNameProperty;
        RelationTargetProperty = relationTargetProperty;
        MultiplicityProperty = multiplicityProperty;
        OutputModel = outputModel;
    }
}