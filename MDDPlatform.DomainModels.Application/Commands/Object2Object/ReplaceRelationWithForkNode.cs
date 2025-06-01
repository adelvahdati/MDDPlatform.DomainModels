using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class ReplaceRelationWithForkNode : ModelOperationPattern
{
    public Guid InputModel { get; set;}
    public string SourceNode { get; set;}
    public string DestinationNode { get; set;}
    public string SourceToDestinationRelation { get; set;}
    public Guid OutputModel { get; set;}
    public string ForkNode { get; set;}
    public string ForkToSourceRelation { get; set;}
    public string ForkToDestinationRelation { get; set;}
    public string ForkNodeInstanceName { get; set;}
    public string ForkNodeInstanceNamePrefix { get; set;}
    public string ForkNodeInstanceNamePostfix { get; set;}

    public ReplaceRelationWithForkNode(Guid inputModel, string sourceNode, string destinationNode, string sourceToDestinationRelation, Guid outputModel, string forkNode, string forkToSourceRelation, string forkToDestinationRelation, string forkNodeInstanceName, string forkNodeInstanceNamePrefix, string forkNodeInstanceNamePostfix)
    {
        InputModel = inputModel;
        SourceNode = sourceNode;
        DestinationNode = destinationNode;
        SourceToDestinationRelation = sourceToDestinationRelation;
        OutputModel = outputModel;
        ForkNode = forkNode;
        ForkToSourceRelation = forkToSourceRelation;
        ForkToDestinationRelation = forkToDestinationRelation;
        ForkNodeInstanceName = forkNodeInstanceName;
        ForkNodeInstanceNamePrefix = forkNodeInstanceNamePrefix;
        ForkNodeInstanceNamePostfix = forkNodeInstanceNamePostfix;
    }
}