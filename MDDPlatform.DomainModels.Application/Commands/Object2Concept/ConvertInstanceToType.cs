using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;

/*
    Example : Convert DomainObjects to DomainConcepts
        TypeOfInstance : type of DomainObject
        PreserverType : true -> new DomainConcept has the same type of DomainObject
                        false -> the type of new DomainConcept is Concept
        PreserverProperty : true -> new DomainConcept has the same property of DomainObject type
                            false -> The properties of DomainObject's type doese not transfer into new DomainConcept
        
        PreserveRelation & PreserverOperation is interpreted in the same way
    
    Conceret Example :
        Input Model : CRAC model
        TypeOfInstance : Command.Concept
        OutputModel : BaseConcept (Built-in)

    Transformation Result :
        PreserverType : true -> 'CreateOrder' concept of type 'Command.Concept'
                        false -> 'CreateOrder' concept of type 'Concept'

    -------------------------------------------------
    TypeOfInstance can be '*' that meanse convert everything
    TypeOfInstance can be sereral types separated by ','
        
*/

public class ConvertInstanceToType : ModelOperationPattern
{
    public Guid InputModel {get;set;}
    public string TypeOfInstance {get;set;}
    public Guid  OutputModel {get;set;}
    public bool PreserveType {get;set;}
    public bool PreserveProperties {get;set;}
    public bool PreserveRelations {get;set;}
    public bool PreserveOperations {get;set;}

    public ConvertInstanceToType(Guid inputModel, string typeOfInstance, Guid outputModel, bool preserveType, bool preserveProperties, bool preserveRelations, bool preserveOperations)
    {
        InputModel = inputModel;
        TypeOfInstance = typeOfInstance;
        OutputModel = outputModel;
        PreserveType = preserveType;
        PreserveProperties = preserveProperties;
        PreserveRelations = preserveRelations;
        PreserveOperations = preserveOperations;
    }
}