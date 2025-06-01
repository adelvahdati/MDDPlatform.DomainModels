using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class CopyUpstreamNodeRelationToDownstreamNodeRelation : ModelOperationPattern
{
    public Guid DownStreamModel { get; set; }
    public Guid UpStreamModel { get; set; }
    public string SourceNode { get; set; } // In downstream model
    public string DestinationNode { get; set; } // In upstream model
    public string SourceToDestinationRelationalDimension { get; set; }
    public string UpStreamNodeRelation { get; set; } // destination node relation
    public string DownStreamNodeRelation { get; set; } // source node relation

    public CopyUpstreamNodeRelationToDownstreamNodeRelation(Guid downStreamModel, Guid upStreamModel, string sourceNode, string destinationNode, string sourceToDestinationRelationalDimension, string upStreamNodeRelation, string downStreamNodeRelation)
    {
        DownStreamModel = downStreamModel;
        UpStreamModel = upStreamModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelationalDimension = sourceToDestinationRelationalDimension;
        UpStreamNodeRelation = upStreamNodeRelation;
        DownStreamNodeRelation = downStreamNodeRelation;
    }
}