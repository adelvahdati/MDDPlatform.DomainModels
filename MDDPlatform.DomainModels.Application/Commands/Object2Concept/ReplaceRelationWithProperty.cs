using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class ReplaceRelationWithProperty : ModelOperationPattern
{
    public Guid InputModel { get; set;}
    public string SourceNode { get; set;}
    public string SourceToDestinationRelation { get; set;}
    public Guid OutputModel { get; set;}

    public ReplaceRelationWithProperty(Guid inputModel, string sourceNode, string sourceToDestinationRelation, Guid outputModel)
    {
        InputModel = inputModel;
        SourceNode = sourceNode;
        SourceToDestinationRelation = sourceToDestinationRelation;
        OutputModel = outputModel;
    }
}