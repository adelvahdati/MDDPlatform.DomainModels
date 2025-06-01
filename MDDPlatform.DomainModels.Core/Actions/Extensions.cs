namespace MDDPlatform.DomainModels.Core.Actions;
public static class Extensions
{

    // ---------------------Domain Concepts -----------------------------------
    public static Dictionary<string,ActionResolver> AddOperationResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new AddOperation();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    public static Dictionary<string,ActionResolver> AddPropertyResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new AddProperty();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    public static Dictionary<string,ActionResolver> AddRelationResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new AddRelation();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    public static Dictionary<string,ActionResolver> CreateConceptResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new CreateConcept();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
        public static Dictionary<string,ActionResolver> RemoveConceptResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new RemoveConcept();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    public static Dictionary<string,ActionResolver> SetConceptAttributeResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new SetConceptAttribute();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    // ---------------------Domain Objects -----------------------------------
    public static Dictionary<string,ActionResolver> AddRelationalDimensionResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new AddRelationalDimension();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    public static Dictionary<string,ActionResolver> CreateInstanceResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new CreateInstance();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    public static Dictionary<string,ActionResolver> SetPropertyResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new SetProperty();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
    public static Dictionary<string,ActionResolver> SetRelationTargetInstanceResolver(this Dictionary<string,ActionResolver> resolvers)
    {
        var action = new SetRelationTargetInstance();
        resolvers.Add(action.ActionSignature.ToString(),action);
        return resolvers;
    }
}