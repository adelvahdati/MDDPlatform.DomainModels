using System.Runtime.Serialization;

namespace MDDPlatform.DomainModels.Core.Exceptions;

[Serializable]
internal class DomainConceptNotExistError : Exception
{
    public string ConceptName {get;}
    public string ConceptType {get;}
    

    public DomainConceptNotExistError(string message, string conceptName, string conceptType):base(message)
    {
        ConceptName = conceptName;
        ConceptType = conceptType;        
    }

}