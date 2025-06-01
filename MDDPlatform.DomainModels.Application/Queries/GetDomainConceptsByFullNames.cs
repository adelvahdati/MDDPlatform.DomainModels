using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainConceptsByFullNames : IQuery<List<DomainConceptDto>>
{
    public Guid DomainModelId {get;}
    public List<string> FullNames {get;}

    public GetDomainConceptsByFullNames(Guid domainModelId, List<string> fullNames)
    {
        DomainModelId = domainModelId;
        FullNames = fullNames;
    }
}