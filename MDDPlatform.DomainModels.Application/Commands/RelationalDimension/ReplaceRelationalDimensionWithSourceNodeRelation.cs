using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class ReplaceRelationalDimensionWithSourceNodeRelation : ModelOperationPattern
{
    public Guid DownStreamModel { get; set;}
    public Guid UpStreamModel { get; set;}
    public string SourceNode { get; set;} // In downstream model
    public string DestinationNode { get; set;} // In upstream model
    public string SourceToDestinationRelationalDimension { get; set;}
    public string RelationName { get; set;} // In downstream model : source node relation
    public string RelationTarget { get; set;} // In downstream model : source node relation target

    public ReplaceRelationalDimensionWithSourceNodeRelation(Guid downStreamModel, Guid upStreamModel, string sourceNode, string destinationNode, string sourceToDestinationRelationalDimension, string relationName, string relationTarget)
    {
        DownStreamModel = downStreamModel;
        UpStreamModel = upStreamModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelationalDimension = sourceToDestinationRelationalDimension;
        RelationName = relationName;
        RelationTarget = relationTarget;
    }
}