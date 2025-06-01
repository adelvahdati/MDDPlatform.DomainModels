using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class AddProperty : ActionResolver
{
    public AddProperty() : base(nameof(AddProperty), 4)
    {

    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        var conceptName = args[0];
        var conceptType = args[1];
        var propName = args[2];
        var propType = args[3];
        domainModel.AddProperty(conceptName,conceptType,propName,propType);
        
    }
}
