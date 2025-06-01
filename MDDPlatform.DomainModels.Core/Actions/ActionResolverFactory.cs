using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Core.Actions;

internal class ActionResolverFactory
{
    private Dictionary<String,ActionResolver> ActionResolvers = new();
    public ActionResolverFactory()
    {
        ActionResolvers.CreateConceptResolver()
                        .AddOperationResolver()
                        .AddPropertyResolver()
                        .AddRelationResolver()
                        .RemoveConceptResolver()
                        .SetConceptAttributeResolver()
                        .AddRelationalDimensionResolver()
                        .CreateInstanceResolver()
                        .SetPropertyResolver()
                        .SetRelationTargetInstanceResolver();

    }
    internal ActionResolver Create(Instruction instruction)
    {
        var signature = Signature.CreateFrom(instruction).ToString();
        
        if(!ActionResolvers.ContainsKey(signature))
            throw new Exception($"Invalid Action. Signature = {signature}");
        
        var resolver = ActionResolvers[signature];
        return resolver;        
    }
}
