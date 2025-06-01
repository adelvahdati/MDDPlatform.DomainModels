using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetModelElements : IQuery<List<ElementDto>>
{
    public Guid DomainModelId {get;set;}

    public GetModelElements(Guid domainModelId)
    {
        DomainModelId = domainModelId;
    }
}