namespace MDDPlatform.DomainModels.Application.Commands;
public class MapObjectPropertyToConceptAttribute : ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string TypeOfInstance {get;set;}
    public Guid  OutputModel {get;set;}

    public MapObjectPropertyToConceptAttribute(Guid inputModel, string typeOfInstance, Guid outputModel)
    {
        InputModel = inputModel;
        TypeOfInstance = typeOfInstance;
        OutputModel = outputModel;
    }
}