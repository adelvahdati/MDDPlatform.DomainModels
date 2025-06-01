using System.Runtime.Serialization;

namespace MDDPlatform.DomainModels.Core.Exceptions;

[Serializable]
internal class DomainsMismatchError : Exception
{
    public Guid DomainOfConcept { get; }
    public Guid DomainOfModel { get; }
    public DomainsMismatchError(string message, Guid domainOfConcept, Guid domainOfModel):base(message)
    {
        DomainOfConcept = domainOfConcept;
        DomainOfModel = domainOfModel;
    }

}