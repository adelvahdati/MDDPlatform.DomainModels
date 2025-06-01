using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class CreateConcept : ActionResolver
{
    public CreateConcept() : base(nameof(CreateConcept), 2)
    {
    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        var conceptName = args[0];
        var conceptType = args[1];
        domainModel.TryCreateDomainConcept(conceptName,conceptType);
        
    }
}
