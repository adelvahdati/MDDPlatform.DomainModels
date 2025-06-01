namespace MDDPlatform.DomainModels.Application.Commands;
public class MergerDomainObjectModels : ModelOperationPattern
{
    public Guid FirstModel {get;set;}
    public Guid SecondModel {get;set;}
    public Guid OutputModel {get;set;}
    
}