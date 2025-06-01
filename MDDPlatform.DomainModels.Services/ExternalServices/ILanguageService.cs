using MDDPlatform.BaseConcepts.Entities;

namespace MDDPlatform.DomainModels.Services;
public interface ILanguageService
{
    Task<List<BaseConcept>> GetLanguageElements(Guid languageId);
}