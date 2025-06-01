using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainModelElements : IQuery<DomainModelElementsDto?>
{
    public Guid DomainModelId { get; }

    public GetDomainModelElements(Guid domainModelId)
    {
        DomainModelId = domainModelId;
    }
}