using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute : ModelOperationPattern
{
    public Guid DownStreamModel { get; set; }
    public Guid UpStreamModel { get; set; }
    public string DownStreamNode { get; set; } // In downstream model
    public string RelationalDimension { get; set; }
    public Guid OutputModel {get;set;}

    public MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute(Guid downStreamModel, Guid upStreamModel, string downStreamNode, string relationalDimension, Guid outputModel)
    {
        DownStreamModel = downStreamModel;
        UpStreamModel = upStreamModel;
        DownStreamNode = downStreamNode;
        RelationalDimension = relationalDimension;
        OutputModel = outputModel;
    }
    public MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute()
    {
        DownStreamModel = Guid.Empty;
        UpStreamModel = Guid.Empty;
        DownStreamNode = string.Empty;
        RelationalDimension = string.Empty;
        OutputModel = Guid.Empty;
    }
}