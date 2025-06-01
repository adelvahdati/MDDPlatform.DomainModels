using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class SetDomainObjectProperty : ICommand
{
    public Guid DomainModelId {get;set;}
    public Guid DomainObjectId {get;set;}    
    public string PropertyName {get;set;}
    public string PropertyValue {get;set;}

    public SetDomainObjectProperty(Guid domainModelId,Guid domainObjectId, string propertyName, string propertyValue)
    {
        DomainObjectId = domainObjectId;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
        DomainModelId = domainModelId;
    }
}