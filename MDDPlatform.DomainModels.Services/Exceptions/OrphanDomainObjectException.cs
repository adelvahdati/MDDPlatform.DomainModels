using System.Runtime.Serialization;

namespace MDDPlatform.DomainModels.Services.Exceptions;

internal class OrphanDomainObjectException : Exception
{
    private Guid DomainObjectId;
    private Guid DomainConceptId;


    public OrphanDomainObjectException(string message, Guid domainConceptId, Guid domainObjectId) : base(message)
    {
        DomainObjectId = domainObjectId;
        DomainConceptId = domainConceptId;
    }

}