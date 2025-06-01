using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;

public class MergeUpstreamNodePropertyWithDownStreamNodeRelation : ModelOperationPattern
{
    public Guid DownStreamModel { get; set; }
    public Guid UpStreamModel { get; set; }
    public string SourceNode { get; set; } // In downstream model
    public string SourceToDestinationRelationalDimension { get; set; }
    public string SourceNodeRelationName {get;set;}
    public Guid OutputModel {get;set;}

    public MergeUpstreamNodePropertyWithDownStreamNodeRelation(Guid downStreamModel, Guid upStreamModel, string sourceNode, string sourceToDestinationRelationalDimension, string sourceNodeRelationName, Guid outputModel)
    {
        DownStreamModel = downStreamModel;
        UpStreamModel = upStreamModel;
        SourceNode = sourceNode;
        SourceToDestinationRelationalDimension = sourceToDestinationRelationalDimension;
        SourceNodeRelationName = sourceNodeRelationName;
        OutputModel = outputModel;
    }
}