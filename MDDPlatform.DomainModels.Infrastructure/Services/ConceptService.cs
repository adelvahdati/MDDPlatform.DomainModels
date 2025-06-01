using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Services;
using MDDPlatform.RestClients;

namespace MDDPlatform.DomainModels.Infrastructure.Services;
public class ConceptService : IConceptService
{
    private IRestClient _restclient;

    public ConceptService(IRestClient restclient)
    {
        _restclient = restclient;
    }

    public async Task<BaseConcept?> GetConcept(Guid conceptId)
    {
        var uri = string.Format("Concept/{0}",conceptId);
        var languageElement = await _restclient.GetAsync<LanguageElementDto>(uri);
        if(languageElement==null)
            return null;

        return languageElement.ToBaseConcept();        
    }
}