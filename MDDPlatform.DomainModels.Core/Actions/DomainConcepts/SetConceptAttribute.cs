using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class SetConceptAttribute : ActionResolver
{
    public SetConceptAttribute() : base(nameof(SetConceptAttribute), 4)
    {
    }

    protected override  void Resolve(DomainModel domainModel, params string[] args)
    {
        var conceptName = args[0];
        var conceptType = args[1];
        var attributeName = args[2];
        var attributeValue = args[3];
        domainModel.SetAttribute(conceptName,conceptType,attributeName,attributeValue);        
    }
}
