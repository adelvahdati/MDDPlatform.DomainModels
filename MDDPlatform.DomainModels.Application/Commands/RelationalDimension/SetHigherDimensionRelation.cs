namespace MDDPlatform.DomainModels.Application.Commands;
public class SetHigherDimensionRelation : ModelOperationPattern
{
    public string UpstreamConcept {get;set;}    // Command.Concept,Event.Concept,CommandHandler.Concept,EventHandler.Concept
    public string DownstreamConcept {get;set;}  // BaseClass.Concept
    public string RelationNameExpression {get;set;} // represent
    public string RelationTargetExpression {get;set;} // UpstreamConcept.Name+.+UpstreamConcept._Type
    public Guid UpstreamModel {get;set;}
    public Guid DownstreamModel {get;set;}

    public SetHigherDimensionRelation(string upstreamConcept, string downstreamConcept, string relationNameExpression, string relationTargetExpression, Guid upstreamModel, Guid downstreamModel)
    {
        UpstreamConcept = upstreamConcept;
        DownstreamConcept = downstreamConcept;
        RelationNameExpression = relationNameExpression;
        RelationTargetExpression = relationTargetExpression;
        UpstreamModel = upstreamModel;
        DownstreamModel = downstreamModel;
    }
}