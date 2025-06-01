using MDDPlatform.BaseConcepts.Entities;

namespace MDDPlatform.DomainModels.Services;
public interface IConceptService
{
    Task<BaseConcept?> GetConcept(Guid conceptId);
}