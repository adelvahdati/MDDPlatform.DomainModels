using System.Runtime.Serialization;

namespace MDDPlatform.DomainModels.Core.Exceptions;

[Serializable]
internal class DomainConceptNotBlongError : Exception
{
    public Guid ConceptModelId { get; }
    public Guid CurrentModelId { get; }
    public DomainConceptNotBlongError(string message, Guid conceptModelId, Guid currentModelId) : base(message)
    {
        ConceptModelId = conceptModelId;
        CurrentModelId = currentModelId;
    }

}