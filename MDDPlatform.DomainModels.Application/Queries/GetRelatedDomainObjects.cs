using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetRelatedDomainObjects : IQuery<List<DomainObjectTupleDto>>
{
    public Guid DomainModelId {get;}
    public List<string> SourceTypes {get;}
    public string RelationName {get;}

    public GetRelatedDomainObjects(Guid domainModelId, List<string> types, string relationName)
    {
        DomainModelId = domainModelId;
        SourceTypes = types;
        RelationName = relationName;
    }
}