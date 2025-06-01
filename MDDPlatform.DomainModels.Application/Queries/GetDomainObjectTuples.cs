using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainObjectTuples : IQuery<List<DomainObjectTupleDto>>
{
    public Guid DomainModelId {get;}
    public string SourceType {get;}
    public string RelationName {get;}
    public string TargetType {get;}

    public GetDomainObjectTuples(Guid domainModelId, string sourceType, string relationName, string targetType)
    {
        DomainModelId = domainModelId;
        SourceType = sourceType;
        RelationName = relationName;
        TargetType = targetType;
    }
}