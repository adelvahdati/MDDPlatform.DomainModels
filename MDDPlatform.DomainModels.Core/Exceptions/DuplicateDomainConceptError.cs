namespace MDDPlatform.DomainModels.Core.Exceptions;

[Serializable]
internal class DuplicateDomainConceptError : Exception
{
    public string ConceptName {get;}
    public string ConceptType {get;}

    public DuplicateDomainConceptError(string message, string conceptName, string conceptType) : base(message)
    {
        ConceptName = conceptName;
        ConceptType = conceptType;
    }
}