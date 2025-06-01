using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Services;
using MDDPlatform.RestClients;

namespace MDDPlatform.DomainModels.Infrastructure.Services;
public class LanguageService : ILanguageService
{
    private IRestClient _restclient;

    public LanguageService(IRestClient restclient)
    {
        _restclient = restclient;
    }

    public async Task<List<BaseConcept>> GetLanguageElements(Guid languageId)
    {
        var uri = string.Format("Language/{0}/Elements",languageId);
        var languageElements = await _restclient.GetAsync<List<LanguageElementDto>>(uri);
        if(languageElements == null)
            return new List<BaseConcept>();
            
        var concepts = languageElements.Select(languageElement => languageElement.ToBaseConcept()).ToList();
        return concepts;
    }
}