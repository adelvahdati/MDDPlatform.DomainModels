using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class AddRelation : ActionResolver
{
    public AddRelation() : base(nameof(AddRelation), 5)
    {
    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        var conceptName = args[0];
        var conceptType = args[1];
        var relationName = args[2];
        var relationTarget = args[3];
        var multiplicity = args[4];

        domainModel.AddRelation(conceptName,conceptType,relationName,relationTarget,multiplicity);        
        
    }
}
