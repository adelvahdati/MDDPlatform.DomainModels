using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class CopyUpstreamNodePropertyToDownstreamNodeRelation : ModelOperationPattern
{
    public Guid DownStreamModel { get; set; }
    public Guid UpStreamModel { get; set; }
    public string SourceNode { get; set; } // In downstream model
    public string DestinationNode { get; set; } // In upstream model
    public string SourceToDestinationRelationalDimension { get; set; }
    public string RelationName { get; set; } // In downstream model : source node relation

    public CopyUpstreamNodePropertyToDownstreamNodeRelation(Guid downStreamModel, Guid upStreamModel, string sourceNode, string destinationNode, string sourceToDestinationRelationalDimension, string relationName)
    {
        DownStreamModel = downStreamModel;
        UpStreamModel = upStreamModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelationalDimension = sourceToDestinationRelationalDimension;
        RelationName = relationName;
    }
}