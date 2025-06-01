using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class MergeUpstreamNodeWithDownStreamNode : ModelOperationPattern
{
    public Guid DownStreamModel { get; set; }
    public Guid UpStreamModel { get; set; }
    public string DownStreamNode { get; set; } // In downstream model
    public string RelationalDimension { get; set; }

    public bool CopyUpstreamNodeProperty {get;set;}
    public bool CopyUpsteramNodeOperation {get;set;}
    public bool CopyDownstreamNodeProperty {get;set;}    
    public bool CopyDownstreamNodeOperation {get;set;}
    public Guid OutputModel {get;set;}

    public MergeUpstreamNodeWithDownStreamNode(Guid downStreamModel, Guid upStreamModel, string downStreamNode, string relationalDimension, bool copyUpstreamNodeProperty, bool copyUpsteramNodeOperation, bool copyDownstreamNodeProperty, bool copyDownstreamNodeOperation, Guid outputModel)
    {
        DownStreamModel = downStreamModel;
        UpStreamModel = upStreamModel;
        DownStreamNode = downStreamNode;
        RelationalDimension = relationalDimension;
        CopyUpstreamNodeProperty = copyUpstreamNodeProperty;
        CopyUpsteramNodeOperation = copyUpsteramNodeOperation;
        CopyDownstreamNodeProperty = copyDownstreamNodeProperty;
        CopyDownstreamNodeOperation = copyDownstreamNodeOperation;
        OutputModel = outputModel;
    }
}