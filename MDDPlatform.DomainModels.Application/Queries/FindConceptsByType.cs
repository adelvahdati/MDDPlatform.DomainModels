using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{
    public class FindConceptsByType : IQuery<IList<DomainConceptDto>>
    {
        public Guid DomainModelId {get;}
        public string Type { get; set;}

        public FindConceptsByType(Guid domainModelId, string type)
        {
            DomainModelId = domainModelId;
            Type = type;
        }
    }
}