namespace MDDPlatform.DomainModels.Application.Commands;
public class SetRelationalDimension :  ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string Element {get;set;}
    public string RelationNameExpression {get;set;}
    public string RelationTargetExpression {get;set;}

    public SetRelationalDimension(Guid inputModel, string element, string relationNameExpression, string relationTargetExpression)
    {
        InputModel = inputModel;
        Element = element;
        RelationNameExpression = relationNameExpression;
        RelationTargetExpression = relationTargetExpression;
    }
}