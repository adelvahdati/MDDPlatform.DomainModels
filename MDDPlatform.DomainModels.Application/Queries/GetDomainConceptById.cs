using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainConceptById : IQuery<DomainConceptDto>
{
    public Guid DomainModelId {get;}
    public Guid DomainConceptId {get;}

    public GetDomainConceptById(Guid domainModelId, Guid domainConceptId)
    {
        DomainModelId = domainModelId;
        DomainConceptId = domainConceptId;
    }
}