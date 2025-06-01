
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainConceptByFullName : IQuery<DomainConceptDto>
{
    public string FullName {get;}
    public Guid DomainModelId {get;}

    public GetDomainConceptByFullName(string fullName, Guid domainModelId)
    {
        FullName = fullName;
        DomainModelId = domainModelId;
    }
}