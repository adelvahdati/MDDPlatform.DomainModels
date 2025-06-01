using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainModelConcepts : IQuery<List<ConceptDto>>{
    public Guid DomainModelId {get;}

    public GetDomainModelConcepts(Guid domainModelId)
    {
        DomainModelId = domainModelId;
    }
}